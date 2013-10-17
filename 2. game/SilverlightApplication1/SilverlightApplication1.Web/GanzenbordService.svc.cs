using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Linq;

namespace SilverlightApplication1.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GanzenbordService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GanzenbordService.svc or GanzenbordService.svc.cs at the Solution Explorer and start debugging.
   
    public class GanzenbordService : IGanzenbordService
    {
        
        private GanzenBordDataContext gb;
        private int playerid;
        
        public GanzenbordService()
        { 
            gb= new GanzenBordDataContext();
        
        }
        public void DoWork()
        {
        }

        public int Gooi()
        {
            Dobbelsteen Dobbelsteen1 = new Dobbelsteen();
            return Dobbelsteen1.GeefWaardeDobbelsteen();
        }


        public void Inloggen(string naam, string wachtwoord)
        {

            
        }


        public void MaakAccount(string naam, string wachtwoord)
        {

            var maxId = (from r in gb.Players
                         select r.PlayerId).Max();
            playerid = maxId + 1;

            

            try
            {

                Player player = new Player();
                player.PlayerNaam = (string)naam;
                player.Wachtwoord =(string) wachtwoord;
                player.PlayerId = playerid;
                gb.Players.InsertOnSubmit(player);
                gb.SubmitChanges();
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
