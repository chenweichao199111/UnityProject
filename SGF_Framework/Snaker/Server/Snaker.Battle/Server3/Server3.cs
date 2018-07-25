using SGF;
using SGF.IPCWork;
using SGF.Server;

namespace Snaker.Battle.Server3
{
    public class Server3 : ServerModule
    {
        private IPCManager m_ipc;

        public override void Start()
        {
            base.Start();

            m_ipc = new IPCManager();
            m_ipc.Init(this.id);
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



        private void Func3(int src, int arg1, string arg2)
        {
            Debuger.Log("arg1:{0}, arg2:{1}", arg1, arg2);

            m_ipc.ReturnError("你失败了");
        }
    }
}