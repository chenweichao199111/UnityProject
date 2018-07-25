using SGF;
using SGF.Server;
using SGF.Time;
using Snaker.GlobalData.Server;
using System;

namespace Snaker.Battle
{
    class Program
    {
        static void Main(string[] args)
        {
            InitDebuger();

            SGFTime.DateTimeAppStart = DateTime.Now;

            ServerManager.Instance.Init("Snaker.Battle");
            ServerManager.Instance.StartServer(ServerID.Server3);
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
