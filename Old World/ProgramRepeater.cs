using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StartProgramTestCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Process process = Process.Start(@"Old_World.exe");
            while (true)
            {
                if(process.HasExited)
                    process = Process.Start(@"Old_World.exe");
                Thread.Sleep(100);
            }
        }
    }
}
