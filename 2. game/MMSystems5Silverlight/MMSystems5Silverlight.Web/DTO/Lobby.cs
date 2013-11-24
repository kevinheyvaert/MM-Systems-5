using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace MMSystems5Silverlight.Web.DTO
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
        [DataMember]
        public bool Start { get; set; }
        [DataMember]
        public int HostID { get; set; }
        [DataMember]
        public int WhoIsTurnId { get; set; }
        [DataMember]
        public int AantalPlayers { get; set; }
    }
}