using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace MMSystems5Silverlight.Web.DTO
{
    [DataContract]
    public class PlayerLobby
    {
        [DataMember]
        int PlayerId { get; set; }

        [DataMember]
        string HostPlayer  { get; set; }
    }
}