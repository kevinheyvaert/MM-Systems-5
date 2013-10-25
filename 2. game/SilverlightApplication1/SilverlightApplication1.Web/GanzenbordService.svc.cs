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

            //check voor welk Id toekennen
            var maxId = (from r in db.Players
                         select r.PlayerId).Max();
            playerid = maxId + 1;


            try
            {

                
                player.PlayerNaam = (string)naam;
                player.Wachtwoord =(string) wachtwoord;
                player.PlayerId = playerid;
                player.IsHost = false;
                db.Players.InsertOnSubmit(player);
                db.SubmitChanges();

                return Inloggen(naam, wachtwoord);
                
            }
            catch (Exception)
            {

                return null;
            }
        }




        public object[] Update(DTO.Lobby Lobby,DTO.Player Player)
        {

            
            

          
           

        
            List<DTO.Lobby> lobbylijst = new List<DTO.Lobby>();
           //Welke lobbys zijn er
            var BeschikbareLobbys = (from l in db.Lobbies
                                     where l.CanJoinLobby == true
                                     select new { l.Hostplayer });
            foreach (var item in BeschikbareLobbys)
            {
                lobbylijst.Add(new DTO.Lobby() { HostPlayer = item.Hostplayer });
            }
           


           //// welke spelers zitten er in die lobby
            var spelersinlobby = (from s in db.Players
                                   where s.Lobby!=null
                                  select new { s.PlayerNaam });

            List<DTO.Player> lobbyinfo = new List<DTO.Player>();
            foreach (var item in spelersinlobby)
            {
                lobbyinfo.Add(new DTO.Player() { PlayerNaam = item.PlayerNaam });
            }


            //on 
            //              new { SomeID = t1.SomeID, SomeName = someName} equals 
            //              new { SomeID = t2.SomeID, SomeName = t2.SomeName} into j1 


           // update[0] = lobbyinfo;

            return null;

            
        }



        public DTO.Player MaakLobby(DTO.Player player)
        {
            try
            {
                if(player.Lobby!=null)
                { 
                    player.Lobby = null; 
                }
                
            Lobby lobby = new Lobby();
            lobby.Hostplayer = player.PlayerNaam;
            lobby.CanJoinLobby = true;

            player.Lobby = player.PlayerNaam;
            player.IsHost = true;
            db.Lobbies.InsertOnSubmit(lobby);
            db.SubmitChanges();

            PlayerLobby playerlobby = new PlayerLobby();
            playerlobby.PlayerId = player.PlayerId;
            playerlobby.HostPlayer = player.PlayerNaam;
            db.PlayerLobbies.InsertOnSubmit(playerlobby);
            db.SubmitChanges();



            var isHost = (from i in db.Players
                          where i.PlayerNaam == player.PlayerNaam && i.Wachtwoord == player.Wachtwoord
                          select i).First();

            isHost.Lobby = player.PlayerNaam;
            isHost.IsHost = true;
            db.SubmitChanges();
            }
            catch (Exception)
            {
                
                throw;
            }

            return player;
        }

       


        public void JoinLobby(DTO.Lobby lobby, DTO.Player player)
        {

            var NextPlayer = (from r in db.Lobbies
                              where r.Hostplayer==lobby.HostPlayer
                              select r).Single();

            if (NextPlayer.Player2 == null)
            {
                NextPlayer.Player2 = player.PlayerId;
            }
            
            
                {
                    var join = (from l in db.Players
                                where l.PlayerId == player.PlayerId && l.IsHost != true
                                select l).First();
                    

                    join.Lobby = lobby.HostPlayer;
                    db.SubmitChanges();
                   

                }

             
            

           
        }




        public void ExitLobby(DTO.Player player)
        {
            var exit = (from l in db.Players
                        where l.Lobby == player.Lobby && l.PlayerId == player.PlayerId
                        select l).First();
            exit.Lobby = null;
            db.SubmitChanges();

        }


        public void StopHost(DTO.Player player)
        {
            var stophost = (from s in db.Players
                            where s.Lobby == player.PlayerNaam
                            select s);

            foreach (var item in stophost)
            {
                item.Lobby = null;
                item.IsHost=false;
            }
           

            var stoplobby = (from s in db.Lobbies
                             where s.Hostplayer==player.PlayerNaam
                             select s).First();

            db.Lobbies.DeleteOnSubmit(stoplobby);
            db.SubmitChanges();
        }
    }
}
