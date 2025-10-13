using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab05_BT2_TcpEchoServerThread
{
    public class FileLogger:ILogger
    {
        private static Mutex mutex = new Mutex();
        private StreamWriter output;

        public FileLogger(String filename)
        {
            output = new StreamWriter(filename, true);
        }

        public void writeEntry(ArrayList entry)
        {
            mutex.WaitOne();
            IEnumerator line = entry.GetEnumerator();
            while (line.MoveNext())
            {
                output.WriteLine(line.Current);
                output.Flush();
            }
            mutex.ReleaseMutex();
        }
        public void writeEntry(String entry)
        {
            mutex.WaitOne();
            output.WriteLine(entry);
            output.Flush();
            mutex.ReleaseMutex();
        }

    }
}
