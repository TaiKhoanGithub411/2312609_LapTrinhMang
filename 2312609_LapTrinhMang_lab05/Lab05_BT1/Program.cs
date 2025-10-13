using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab05_BT1
{
    class Program
    {
        static void Main(string[] args)
        {
            MyThreadClass mtc1 = new MyThreadClass("day la tieu trinh 1");
            Thread thread1 = new Thread(new ThreadStart(mtc1.runMyThread));
            thread1.Start();

            MyThreadClass mtc2 = new MyThreadClass("day la tieu trinh 2");
            Thread thread2 = new Thread(new ThreadStart(mtc2.runMyThread));
            thread2.Start();

            Console.ReadKey();
        }
    }
}
