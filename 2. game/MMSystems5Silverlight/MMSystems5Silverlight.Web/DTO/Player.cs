﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace MMSystems5Silverlight.Web.DTO
{
    [DataContract]
    public class Player
    {
        [DataMember]
        public int PlayerId { get; set; }
        [DataMember]
        public string PlayerNaam { get; set; }
        [DataMember]
        public string Wachtwoord { get; set; }
        [DataMember]
        public int Locatie { get; set; }
        [DataMember]
        public int Gewonnen { get; set; }
        [DataMember]
        public int Verloren { get; set; }

    }
}