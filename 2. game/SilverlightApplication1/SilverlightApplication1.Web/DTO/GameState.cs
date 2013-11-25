using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SilverlightApplication1.Web.DTO
{
    [DataContract]
    public class GameState
    {

        [DataMember]
        public List<Player> players { get; set; }
        [DataMember]
        public Player turn { get; set; }
        [DataMember]
        public bool Start { get; set; }

    }
}