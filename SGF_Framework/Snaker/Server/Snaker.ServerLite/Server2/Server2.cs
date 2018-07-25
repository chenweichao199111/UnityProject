using SGF;
using SGF.IPCWork;
using SGF.Server;
using Snaker.GlobalData.Server;

namespace Snaker.ServerLite.Server2
{
    public class Server2:ServerModule
    {
        private IPCManager m_ipc;

        public override void Start()
        {
            base.Start();
            m_ipc = new IPCManager();
            m_ipc.Init(id);
            m_ipc.Start();
            m_ipc.AddRPCListener(this);
        }

        public override void Stop()
        {
            if (m_ipc != null)
            {
                m_ipc.Clean();
                m_ipc = null;
            }

            base.Stop();
        }

        public override void Tick()
        {
            base.Tick();
            m_ipc.Tick();
        }



        private void Func2(int src, string arg1, int arg2)
        {
            Debuger.Log("arg1:{0}, arg2:{1}", arg1, arg2);

            //调用Server3的RPC
            m_ipc.Invoke(ServerID.Server3, "Func3", 789, "我是参数2");

            m_ipc.Return("你成功了");
        }

        private void OnFunc3Error(int src, string errinfo, int errcode)
        {
            Debuger.LogError("errinfo:{0}, errcode:{1}", errinfo, errcode);
        }


        
    }
}