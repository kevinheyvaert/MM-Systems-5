using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace MMSystems5Game
{
    public class Network
    {
        public Network()
        { 
        
        }
        public bool Connectie()
        {
           
                return NetworkInterface.GetIsNetworkAvailable();
        }

    }
}
