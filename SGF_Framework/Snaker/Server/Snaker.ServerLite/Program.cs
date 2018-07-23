using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snaker2.ServerLite
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerDemo demo = new ServerDemo();
            demo.Init();

            while (true)
            {
                demo.Tick();
                Thread.Sleep(1);
            }
        }
    }

}
