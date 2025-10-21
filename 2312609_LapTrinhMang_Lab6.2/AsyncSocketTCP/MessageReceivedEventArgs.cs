using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSocketTCP
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Message { get; set; }
        public string ClientInfo { get; set; }

        public MessageReceivedEventArgs(string message, string clientInfo)
        {
            Message = message;
            ClientInfo = clientInfo;
        }
    }
}
