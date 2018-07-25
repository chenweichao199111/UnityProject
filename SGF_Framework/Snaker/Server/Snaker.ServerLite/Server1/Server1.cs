using SGF;
using SGF.IPCWork;
using SGF.Server;
using Snaker.GlobalData.Server;

namespace Snaker.ServerLite.Server1
{
    public class Server1 : ServerModule
    {
        private IPCManager m_ipc;

        private bool m_hasInvoke = false;


        public override void Start()
        {
            base.Start();

            m_ipc = new IPCManager();
            m_ipc.Init(id);
            m_ipc.AddRPCListener(this);
            m_ipc.Start();
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

            if (!m_hasInvoke)
            {
                m_hasInvoke = true;

                //调用Server2的RPC
                m_ipc.Invoke(ServerID.Server2, "Func2", "我是参数1", 123456);
            }
        }

        private void OnFunc2(int src, string info)
        {
            Debuger.LogWarning(info);
        }
    }
}