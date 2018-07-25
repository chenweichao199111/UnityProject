using SGF;
using SGF.Server;
using SGF.Time;
using Snaker.GlobalData.Server;
using System;

namespace Snaker.ServerLite
{
    class Program
    {
        static void Main(string[] args)
        {
            InitDebuger();

            SGFTime.DateTimeAppStart = DateTime.Now;

            ServerManager.Instance.Init("Snaker.ServerLite");
            ServerManager.Instance.StartServer(ServerID.Server1);
            ServerManager.Instance.StartServer(ServerID.Server2);

            MainLoop.Run();

            ServerManager.Instance.StopAllServer();
        }

        static void InitDebuger()
        {
            Debuger.Init(AppDomain.CurrentDomain.BaseDirectory + "/ServerLog/");
            Debuger.EnableLog = true;
            Debuger.EnableSave = true;
            Debuger.Log();
        }
    }

}
