using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab05_BT2_TcpEchoServerThread
{
    class ConsoleLogger:ILogger
    {
        private static Mutex mutex = new Mutex();

        public void writeEntry(ArrayList entry)
        {
            mutex.WaitOne();
            IEnumerator line = entry.GetEnumerator();
            while (line.MoveNext())
                Console.WriteLine(line.Current);
            Console.Out.Flush();
            mutex.ReleaseMutex();
        }

        public void writeEntry(String entry)
        {
            mutex.WaitOne();
            Console.WriteLine(entry);
            mutex.ReleaseMutex();
        }

    }
}
