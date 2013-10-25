using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace SilverlightApplication1.Web.DTO
{
    [DataContract]
    public class Lobby
    {
        [DataMember]
        public List<Player> PlayersInLobby { get; set; }
        [DataMember]
        public string HostPlayer { get; set; }
        [DataMember]
        public bool CanJoinLobby { get; set; }
    }
}