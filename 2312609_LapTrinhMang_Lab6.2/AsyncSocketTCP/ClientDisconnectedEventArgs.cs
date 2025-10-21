using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSocketTCP
{
    public class ClientDisconnectedEventArgs : EventArgs
    {
        public string DisconnectedClient { get; set; }
        public int RemainingClients { get; set; }

        public ClientDisconnectedEventArgs(string disconnectedClient, int remainingClients)
        {
            DisconnectedClient = disconnectedClient;
            RemainingClients = remainingClients;
        }
    }
}
