using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SilverlightApplication1.Web.DTO
{
    [DataContract]
    public class PlayerLobby
    {
        [DataMember]
        public int PlayerId { get; set; }
    [DataMember]
        public string HostPlayer { get; set; }
    }
}