using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MMSystems5Silverlight.Web
{
    public class Player 
    {
        public Player()
        {


        }
        public string UserName { get; set; }
        public bool Beurt { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public int Id { get; set; }
        public int Locatie { get; set; }
    }


}
