using System.Collections.Generic;
using System.Text;
using SGF;
using SGF.Common;
using SGF.Network.Core.RPCLite;
using SGF.Network.General.Server;
using Snaker.GlobalData.Data;
using Snaker.ServerLite.ZoneServer.Online;

namespace Snaker.Server.ZoneServer
{
    public class RoomManager:Singleton<RoomManager>
    {
        private ServerContext m_context;
        private List<Room> m_listRoom = new List<Room>();


        public void Init(ServerContext context)
        {
            m_context = context;
            m_context.net.RegisterRPCListener(this);
            m_context.ipc.AddRPCListener(this);
        }

        public void Clean()
        {
            if (m_context != null)
            {
                m_context.net.UnRegisterRPCListener(this);
                m_context.ipc.RemoveRPCListener(this);
                m_context = null;
            }
        }

        public void Dump()
        {

            StringBuilder sb = new StringBuilder();
            Room[] list = m_listRoom.ToArray();
            for (int i = 0; i < list.Length; i++)
            {
                sb.AppendLine("\t" + list[i].DumpString("\t"));
            }

            Debuger.LogWarning("\nRooms ({0}):\n{1}", m_listRoom.Count, sb);

        }



        //=========================================================================
        [RPCRequest]
        private void UpdateRoomList(ISession session)
        {

            List<RoomData> list = new List<RoomData>();
            for (int i = 0; i < m_listRoom.Count; i++)
            {
                list.Add(m_listRoom[i].data);
            }

            RoomListData data = new RoomListData();
            data.rooms = list;

            m_context.net.Return(data);
        }


        //=========================================================================
        [RPCRequest]
        private void CreateRoom(ISession session, string roomName)
        {
            Room room = new Room();
            uint userId = session.uid;
            UserData ud = OnlineManager.Instance.GetUserData(userId);
            room.Create(userId, ud.name, session, roomName);
            m_listRoom.Add(room);

            m_context.net.Return(room.data);
        }

        [RPCRequest]
        private void JoinRoom(ISession session, uint roomId)
        {
            var userId = session.uid;
            Room room = GetRoom(roomId);
            if (room != null)
            {
                UserData ud = OnlineManager.Instance.GetUserData(userId);

                room.AddPlayer(userId, ud.name, session);
                ISession[] listSession = room.GetSessionList();

                m_context.net.Invoke(listSession, "NotifyRoomUpdate", room.data);
            }
            else
            {
                m_context.net.ReturnError("房间不存在", roomId);
            }
        }

        
        [RPCRequest]
        private void ExitRoom(ISession session,  uint roomId)
        {
            var userId = session.uid;
            Room room = GetRoom(roomId);
            if (room != null)
            {
                room.RemovePlayer(userId);

                if (room.GetPlayerCount() > 0)
                {
                    ISession[] listSession = room.GetSessionList();
                    m_context.net.Invoke(listSession, "NotifyRoomUpdate", room.data);
                }
            }
        }


        [RPCRequest]
        private void RoomReady(ISession session, uint roomId, bool ready)
        {
            var userId = session.uid;
            Room room = GetRoom(roomId);
            if (room != null)
            {
                room.SetReady(userId, ready);
                ISession[] listSession = room.GetSessionList();
                m_context.net.Invoke(listSession, "NotifyRoomUpdate", room.data);
            }
            else
            {
                m_context.net.ReturnError("房间不存在", (int)roomId);
            }
        }

        //=========================================================

        private void StartGame(ISession session, uint roomId)
        {
            var userId = session.uid;
            Room room = GetRoom(roomId);
            if (room != null)
            {
                if (room.data.owner == userId)
                {
                    if (room.CanStartGame())
                    {
                        PVPStartParam param = new PVPStartParam();

                        /*
                        FSPGame fspGame = FSPServer.Instance.CreateGame(room.data.id);
                        var list = room.data.players;
                        for (int i = 0; i < list.Count; i++)
                        {
                            FSPPlayer fspPlayer = fspGame.AddPlayer(list[i].id);
                            list[i].sid = fspPlayer.sid;
                        }

                        param.fspParam = FSPServer.Instance.GetParam();
                        */

                        param.gameParam = room.GetGameParam();
                        param.players = room.data.players;

                        ISession[] listSession = room.GetSessionList();
                        m_context.net.Invoke(listSession, "NotifyGameStart", param);

                    }
                    else
                    {
                        m_context.net.ReturnError("还有玩家未准备", roomId);
                    }
                }
                else
                {
                    m_context.net.ReturnError("你不是房主，只有房主可以启动游戏", roomId);
                }
            }
            else
            {
                m_context.net.ReturnError("房间不存在", roomId);
            }
        }



        //=========================================================

        private Room GetRoom(uint roomId)
        {
            for (int i = 0; i < m_listRoom.Count; i++)
            {
                if (m_listRoom[i].data.id == roomId)
                {
                    return m_listRoom[i];
                }
            }
            return null;
        }


    }
}