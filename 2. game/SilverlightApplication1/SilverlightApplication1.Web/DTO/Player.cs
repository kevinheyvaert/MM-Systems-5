﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SilverlightApplication1.Web.DTO
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
        [DataMember]
        public string Lobby { get; set; }
        [DataMember]
        public bool IsHost { get; set; }
        [DataMember]
        public int HostID { get; set; }
           
        }
  }
