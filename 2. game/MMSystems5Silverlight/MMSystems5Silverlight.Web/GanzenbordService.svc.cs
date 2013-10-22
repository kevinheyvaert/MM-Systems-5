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
        private GanzenBordCloudSqlDataContext gb;
        private int playerid;
        Player player = new Player();

        public GanzenbordService()
        {
            gb = new GanzenBordCloudSqlDataContext();

        }
        
        public void DoWork()
        {
        }
        
        public int  Gooi()
        {   
            
            Dobbelsteen Dobbelsteen1 = new Dobbelsteen();
            return Dobbelsteen1.GeefWaardeDobbelsteen();
        }

       

        public DTO.Player Inloggen(string naam, string wachtwoord)
        {
            List<DTO.Player> playerList = new List<DTO.Player>();
            try
            {
                var usercontrol = from u in gb.Players
                                  where u.PlayerNaam == naam && u.Wachtwoord == wachtwoord
                                  select new { u.PlayerNaam, u.Gewonnen, u.Verloren, u.Wachtwoord, u
                                  .PlayerId};

                foreach (var item in usercontrol)
                {
                    playerList.Add(new DTO.Player() { PlayerNaam = item.PlayerNaam, Wachtwoord = item.Wachtwoord });
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

        
        public DTO.Player MaakAccount(string PlayerNaam, string Wachtwoord)
        {
            var maxId = (from r in gb.Players
                         select r.PlayerId).Max();
            playerid = maxId + 1;

            try
            {

                Player player = new Player();
                player.PlayerNaam = (string)PlayerNaam;
                player.Wachtwoord = (string)Wachtwoord;
                player.PlayerId = playerid;
                gb.Players.InsertOnSubmit(player);
                gb.SubmitChanges();

                return Inloggen(PlayerNaam, Wachtwoord);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<DTO.Lobby> BeschikbareLobbys()
        {
            var BeschikbareLobbys = (from l in gb.Lobbies
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
                lobby.Hostplayer = (string)player.PlayerNaam;
                lobby.CanJoinLobby = true;
                gb.Lobbies.InsertOnSubmit(lobby);
                gb.SubmitChanges();

                PlayerLobby playerlobby = new PlayerLobby();
                playerlobby.PlayerId = player.PlayerId;
                playerlobby.HostPlayer = player.PlayerNaam;
                gb.PlayerLobbies.InsertOnSubmit(playerlobby);
                gb.SubmitChanges();

                var isHost = (from i in gb.Players
                              where i.PlayerNaam == player.PlayerNaam && i.Wachtwoord == player.Wachtwoord
                              select i).First();

                isHost.Lobby = player.PlayerNaam;
                isHost.IsHost = true;
                gb.SubmitChanges();
              
               
            }

            catch
            {

            }
            return player;
        }


        public List<DTO.Player> LobbyInfo(DTO.Lobby lobby)
        {

            var spelersinlobby = (from s in gb.Players 
                                  where s.Lobby==lobby.HostPlayer 
                                  select new {s.PlayerNaam});

            List<DTO.Player> lobbyinfo = new List<DTO.Player>();
            foreach (var item in spelersinlobby)
            {
               lobbyinfo.Add(new DTO.Player() { PlayerNaam = item.PlayerNaam});
            }

            return lobbyinfo;

        }


        public void JoinLobby(DTO.Lobby lobby, DTO.Player player)
        {
            try
            {
                var join = (from l in gb.Players
                            where l.PlayerId == player.PlayerId
                            select l).First();


                join.Lobby = lobby.HostPlayer;
                gb.SubmitChanges();

            }

            catch 
            { }
        }
    }
}
