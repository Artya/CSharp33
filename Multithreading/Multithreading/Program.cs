using System;
using System.Collections.Generic;
using System.Threading;

namespace Multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            var thread1 = new Thread(ThreadManipulator.AddingOne);
            var thread2 = new Thread(ThreadManipulator.AddingOne);
            var thread3 = new Thread(ThreadManipulator.AddingCustomValue);
            var thread4 = new Thread(ThreadManipulator.Stop);

            var threads = new Thread[3] { thread1, thread2, thread3 };

            thread4.Start(threads);
            thread1.Start(10);
            thread2.Start(20);
            thread3.Start(new int[2] { 15, 5 });

            thread1.Join();
            thread2.Join();
            thread3.Join();
        }
    }
}
