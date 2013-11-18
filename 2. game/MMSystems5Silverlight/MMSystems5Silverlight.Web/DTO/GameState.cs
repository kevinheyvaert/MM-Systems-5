using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace MMSystems5Silverlight.Web.DTO
{
    [DataContract]
    public class GameState
    {
        [DataMember]
        public List<Player> players { get; set; }
        [DataMember]
        public Player turn { get; set; }

    }
}