using SGF;
using SGF.Common;
using SGF.Network.General.Client;
using Snaker.GlobalData.Data;
using Snaker.GlobalData.Proto;

namespace Snaker.Services.Online
{
    public class OnlineManager:Singleton<OnlineManager>
    {
        private NetManager m_net;


        public void Init()
        {
            m_net = new NetManager();
            m_net.Init(typeof(KCPConnection), 1, 2939);
            m_net.Connect("127.0.0.1", 4540);
            m_net.RegisterRPCListener(this);

            GlobalEvent.onUpdate.AddListener(OnUpdate);
        }

        public void Clean()
        {
            GlobalEvent.onUpdate.RemoveListener(OnUpdate);
            if (m_net != null)
            {
                m_net.Clean();
                m_net = null;
            }
        }

        private void OnUpdate()
        {
            m_net.Tick();
        }

        public void Login(int id, string name)
        {
            LoginReq req = new LoginReq();
            req.id = id;
            req.name = name;
            m_net.Send<LoginReq, LoginRsp>(ProtoCmd.LoginReq, req, OnLoginRsp, 30, OnLoginErr);
        }

        private void OnLoginRsp(LoginRsp rsp)
        {
            Debuger.Log("ret:{0}, msg:{1}", rsp.ret, rsp.msg);
        }

        private void OnLoginErr(int errcode)
        {
            Debuger.LogError("ErrCode:{0}", errcode);
        }

        

        public void TestRPC()
        {
            m_net.Invoke("StartGameRequest1", 1,"abc");
        }

        private void NotifyStartGame(string a, PVPStartParam param)
        {
            
            Debuger.Log("a:{0}, param.gameId:{1}, param.roomName:{2}", a, param.gameId, param.roomName);
        }
    }
}