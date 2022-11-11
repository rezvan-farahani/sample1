using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Printer
    {
        private object threadLock = new object();
        public void PrintNumbers()
        {
            //lock (threadLock)
            //{
            Monitor.Enter(threadLock);
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    Random r = new Random();
                    //Thread.Sleep(1000 * r.Next(5));
                    Console.Write("{0}, ", i);
                }
                Console.WriteLine();
            }
            finally
            {
                Monitor.Exit(threadLock);
            }
           
           // }
           
            //    Console.WriteLine("-> {0} is executing PrintNumbers()",Thread.CurrentThread.Name);
            //    Console.Write("Your numbers: ");
            //    for (int i = 0; i < 10; i++)
            //    {
            //        Console.Write("{0}, ", i);
            //        Thread.Sleep(2000);
            //    }
            //    Console.WriteLine();
        }
    }
}
