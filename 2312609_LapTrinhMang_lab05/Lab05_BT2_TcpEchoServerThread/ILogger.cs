using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab05_BT2_TcpEchoServerThread
{
    public interface ILogger
    {
        void writeEntry(ArrayList entry);
        void writeEntry(String entry);
    }
}
