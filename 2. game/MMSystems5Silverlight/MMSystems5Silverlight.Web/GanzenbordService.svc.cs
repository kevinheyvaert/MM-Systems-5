using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Collections.Generic; // For generic collections like List.
using System.Data.SqlClient;      // For the database connections and objects.
using System.Xml;
using System.Linq;


namespace MMSystems5Silverlight.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GanzenbordService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GanzenbordService.svc or GanzenbordService.svc.cs at the Solution Explorer and start debugging.
    public class GanzenbordService : IGanzenbordService
    {
        Player player = new Player();
        private GanzenBordCloudSqlDataContext db;

        private int playerid;

        public GanzenbordService()
        {

            db = new GanzenBordCloudSqlDataContext();

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
                                  select new { u.PlayerNaam, u.Gewonnen, u.Verloren, u.Wachtwoord, u.PlayerId, u.Lobby, u.IsHost };

                foreach (var item in usercontrol)
                {
                    playerList.Add(new DTO.Player() { PlayerNaam = item.PlayerNaam, Wachtwoord = item.Wachtwoord, PlayerId = item.PlayerId, Lobby = item.Lobby });
                }
                if (playerList.Count() > 0)
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
                player.Wachtwoord = (string)wachtwoord;
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




        public List<DTO.Lobby> BeschikbareLobbys()
        {
            var BeschikbareLobbys = (from l in db.Lobbies
                                     where l.CanJoinLobby == true
                                     select new { l.Hostplayer });

            List<DTO.Lobby> LobbyList = new List<DTO.Lobby>();

            foreach (var item in BeschikbareLobbys)
            {
                LobbyList.Add(new DTO.Lobby() { HostPlayer = item.Hostplayer });
            }

            return LobbyList;


        }


        public DTO.Player MaakLobby(DTO.Player player)
        {
            try
            {
                Lobby lobby = new Lobby();
                lobby.Hostplayer = player.PlayerNaam;
                lobby.CanJoinLobby = true;
                lobby.AantalPlayers = 1;
                db.Lobbies.InsertOnSubmit(lobby);
                db.SubmitChanges();

                var isHost = (from i in db.Players
                              where i.PlayerId == player.PlayerId
                              select i).First();

                isHost.Lobby = player.PlayerNaam;
                isHost.IsHost = true;
                db.SubmitChanges();
                updatelobby(player.Lobby);
                return player;
                
            }
            catch (Exception)
            {

                return null;
            }

            
            
        }

        public List<DTO.Player> LobbyInfo(DTO.Lobby lobby)
        {

            var spelersinlobby = (from s in db.Players
                                  where s.Lobby == lobby.HostPlayer
                                  select new { s.PlayerNaam });

            List<DTO.Player> lobbyinfo = new List<DTO.Player>();
            foreach (var item in spelersinlobby)
            {
                lobbyinfo.Add(new DTO.Player() { PlayerNaam = item.PlayerNaam });
            }

            return lobbyinfo;

        }

        private void updatelobby(string lobby)
        {

            var ad = (from i in db.Lobbies
                      where i.Hostplayer == lobby
                      select i).Single();
           
            var count =(from i in db.Players
                        where i.Lobby==lobby
                        select i).Count();



            if (ad.AantalPlayers == 4)
            {
                updateaantal(lobby, false);
                ad.AantalPlayers = count;
            }


            else if (ad.AantalPlayers < 4)
            {
                updateaantal(lobby, true);
                ad.AantalPlayers = count;
                
            }

            db.SubmitChanges();


        }

        private void updateaantal(string lobby, bool canjoin)
        {

            var ad = (from i in db.Lobbies
                      where i.Hostplayer == lobby
                      select i).Single();

            if (canjoin == false)
            {
                ad.CanJoinLobby = false;
            }
            else if (canjoin == true)
            {
                ad.CanJoinLobby = true;
            }
            db.SubmitChanges();


        }



        public void JoinLobby(DTO.Lobby lobby, DTO.Player player)
        {

            if (player.IsHost == true)
           {
                StopHost(player);
            }
           


            var join = (from l in db.Players
                        where l.PlayerId == player.PlayerId
                        select l).Single();

            
            join.Lobby = lobby.HostPlayer;
            db.SubmitChanges();
            updatelobby(lobby.HostPlayer);



        }


        public void ExitLobby(DTO.Player player)
        {
            var exit = (from l in db.Players
                        where l.Lobby == player.Lobby && l.PlayerId == player.PlayerId
                        select l).First();
            
            exit.Lobby = null;
            db.SubmitChanges();
            updatelobby(player.Lobby);

        }


        public void StopHost(DTO.Player player)
        {
            var stophost = (from s in db.Players
                            where s.Lobby == player.PlayerNaam
                            select s);

            foreach (var item in stophost)
            {
                item.Lobby = null;
                item.IsHost = false;
            }


            var stoplobby = (from s in db.Lobbies
                             where s.Hostplayer == player.PlayerNaam
                             select s).First();

            db.Lobbies.DeleteOnSubmit(stoplobby);
            db.SubmitChanges();
        }
    }
}
