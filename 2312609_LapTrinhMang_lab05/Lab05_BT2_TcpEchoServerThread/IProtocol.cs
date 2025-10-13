using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05_BT2_TcpEchoServerThread
{
    public interface IProtocol
    {
        void handleclient();
    }
}
