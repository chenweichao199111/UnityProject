using SGF;
using SGF.Network.General.Server;
using Snaker2.GlobalData.Data;
using Snaker2.GlobalData.Proto;

namespace Snaker2.ServerLite
{
    public class ServerDemo
    {
        private NetManager m_net;
        public void Init()
        {
            Debuger.Init();
            Debuger.EnableLog = true;

            Debuger.Log();
            m_net = new NetManager();
            m_net.Init(4540);
            m_net.SetAuthCmd(ProtoCmd.LoginReq);
            m_net.AddListener<LoginReq>(ProtoCmd.LoginReq, OnLoginReq);

            m_net.RegisterRPCListener(this);
        }

        public void Tick()
        {
            m_net.Tick();
        }

        private void OnLoginReq(ISession session, uint index, LoginReq req)
        {
            Debuger.Log("id:{0}, name:{1}", req.id, req.name);

            LoginRsp rsp = new LoginRsp();
            rsp.ret = 0;
            rsp.msg = "Hi,"+req.name+" 你登录成功了！";

            //假设鉴权通过了
            session.SetAuth((uint)req.id);

            m_net.Send(session, index, ProtoCmd.LoginRsp, rsp);

        }


        private void StartGameRequest(ISession session, int a, string b)
        {
            Debuger.Log("a:{0}, b:{1}", a, b);

            PVPStartParam param = new PVPStartParam();
            param.gameId = 12345;
            param.roomName = "GoodRoom";

            m_net.Invoke(session, "NotifyStartGame", "ok", param);
        }
    }
}