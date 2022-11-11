using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    //change(1)

    //change(2)

    public delegate int BinaryOp(int x, int y);
    class Program
    {
        private static bool isDone = false;
        private static AutoResetEvent waitHandle = new AutoResetEvent(false);
        static Printer p = new Printer();
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(p.PrintNumbers))
                {
                    Name = $"Worker thread #{i}"
                };
            }
            foreach (Thread t in threads)
                t.Start();
            Console.ReadLine();


            //Console.WriteLine("***** Adding with Thread objects *****");
            //Console.WriteLine("ID of thread in Main(): {0}",Thread.CurrentThread.ManagedThreadId);
            ////AddParams ap = new AddParams(10, 10);
            ////Thread t = new Thread(new ParameterizedThreadStart(Add));
            //Thread bgroundThread = new Thread(new ThreadStart(p.PrintNumbers));
            ////bgroundThread.IsBackground = true;
            //bgroundThread.Start();
            ////t.Start(ap);
            ////waitHandle.WaitOne();
            ////Thread.Sleep(1);
            ////Console.ReadLine();
        }

        static void Add(object data)
        {
            if (data is AddParams)
            {
                Console.WriteLine("ID of thread in Add(): {0}",
                Thread.CurrentThread.ManagedThreadId);
                AddParams ap = (AddParams)data;
                Console.WriteLine("{0} + {1} is {2}",
                ap.a, ap.b, ap.a + ap.b);
                waitHandle.Set();
            }
        }

        static void AddCompleteDelegate()
        {
            Console.WriteLine(Thread.GetDomain());
            Console.WriteLine(Thread.CurrentContext);

            Console.WriteLine("***** Synch Delegate Review *****");
            Console.WriteLine("Main() invoked on thread {0}.",
            Thread.CurrentThread.ManagedThreadId);
            BinaryOp b = new BinaryOp(Add);
            //int answer = b(10, 10);//Add(10, 10);
            //IAsyncResult ar = b.BeginInvoke(10, 10, new AsyncCallback(AddComplete), null);
            IAsyncResult ar = b.BeginInvoke(10, 10, new AsyncCallback(AddComplete), "Main() thanks you for adding these numbers.");
            //while (!isDone)//!ar.IsCompleted)
            //{
            //    Console.WriteLine("Doing more work in Main()!");
            //    Thread.Sleep(1000);
            //}
            //int answer = b.EndInvoke(ar);
            //Console.WriteLine("10 + 10 is {0}.", answer);
            Console.ReadLine();
        }

        static int Add(int x, int y)
        {
            Console.WriteLine("Add() invoked on thread {0}.",Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
            return x + y;
        }

        static void AddComplete(IAsyncResult iar)
        {
            Console.WriteLine("AddComplete() invoked on thread {0}.", Thread.CurrentThread.
            ManagedThreadId);
            AsyncResult ar = (AsyncResult)iar;
            BinaryOp b=(BinaryOp)ar.AsyncDelegate;
            string message = (string)iar.AsyncState;
            Console.WriteLine(message);
            Console.WriteLine("10 + 10 is {0}.", b.EndInvoke(iar));
            isDone = true;
        }
    }
}
