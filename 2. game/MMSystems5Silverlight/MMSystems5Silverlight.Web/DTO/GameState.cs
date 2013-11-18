using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMSystems5Silverlight.Web.DTO
{
    public class GameState
    {
        public List<Player> players { get; set; }
        public Player turn { get; set; }
    }
}