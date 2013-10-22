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

        Player player = new Player();
        private DataCloudDataContext db;
        private int playerid;
        
        public GanzenbordService()
        { 
          
            db = new DataCloudDataContext();
        
        }
        public void DoWork()
        {
        }

        public int Gooi()
        {
            Dobbelsteen Dobbelsteen1 = new Dobbelsteen();
            return Dobbelsteen1.GeefWaardeDobbelsteen();
        }


        public DTO.Player Inloggen(string naam, string wachtwoord)
        {
            List<DTO.Player> playerList = new List<DTO.Player>();
            try
            {

 

                var usercontrol = from u in db.Players
                                  where u.PlayerNaam == naam && u.Wachtwoord == wachtwoord
                                  select new {u.PlayerNaam,u.Gewonnen,u.Verloren,u.Wachtwoord, u.PlayerId};

                foreach (var item in usercontrol)
                {
                    playerList.Add(new DTO.Player() { PlayerNaam = item.PlayerNaam, Wachtwoord = item.Wachtwoord, PlayerId=item.PlayerId });
                }
                if(playerList.Count()>0)
                {
                    return playerList.First();
                }
                else 
                    return null;

                                      


              
            }
            catch (Exception)
            {
                return null;
                
            }
            
        }


        public DTO.Player MaakAccount(string naam, string wachtwoord)
        {

            var maxId = (from r in db.Players
                         select r.PlayerId).Max();
            playerid = maxId + 1;

            

            try
            {

                
                player.PlayerNaam = (string)naam;
                player.Wachtwoord =(string) wachtwoord;
                player.PlayerId = playerid;
                db.Players.InsertOnSubmit(player);
                db.SubmitChanges();

                return Inloggen(naam, wachtwoord);
                
            }
            catch (Exception)
            {

                return null;
            }
        }




        public List<DTO.Lobby> BeschikbareLobbys()
        {
            var BeschikbareLobbys = (from l in db.Lobbies
                                     where l.CanJoinLobby == true
                                      select new {l.Hostplayer});

            List<DTO.Lobby> LobbyList = new List<DTO.Lobby>();

            foreach (var item in BeschikbareLobbys)
            {
               LobbyList.Add(new DTO.Lobby() { HostPlayer=item.Hostplayer });
            }

            return LobbyList;

            
        }


        public DTO.Player MaakLobby(DTO.Player player)
        {
            Lobby lobby = new Lobby();
            lobby.Hostplayer = (string)player.PlayerNaam;
            lobby.CanJoinLobby = true;
            db.Lobbies.InsertOnSubmit(lobby);
            db.SubmitChanges();

            PlayerLobby playerlobby = new PlayerLobby();
            playerlobby.PlayerId = player.PlayerId;
            playerlobby.HostPlayer = player.PlayerNaam;
            db.PlayerLobbies.InsertOnSubmit(playerlobby);
            db.SubmitChanges();

            return player;
            
        }
    }
}
