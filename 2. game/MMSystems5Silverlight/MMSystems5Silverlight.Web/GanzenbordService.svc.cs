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
        Dobbelsteen Dobbelsteen1;
        int[] DobbelEnLocatie;

        private int playerid;

        public GanzenbordService()
        {
            
            Dobbelsteen1 = new Dobbelsteen();
            db = new GanzenBordCloudSqlDataContext();


        }
        public void DoWork()
        {
        }

        public int [] Gooi(DTO.Player player)
        {
            DobbelEnLocatie[0] = Dobbelsteen1.GeefWaardeDobbelsteen();
            //gooi.Content = Dice(e.Result);
            var speler = (from s in db.Players
                          where s.PlayerId == player.PlayerId
                          select s).Single();

            speler.Locatie += DobbelEnLocatie[0]; 
           // Speler.Locatie = Speler.Locatie + e.Result;

            //If (Speler.Locatie == 5, 9, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59)
            //{
            //  NOG IS GOOIEN MET HET GEGOOIDE AANTAL
            //}

            if (speler.Locatie == 6)
            {
                speler.Locatie = 12;
               // MessageBox.Show("Brug : Ga naar 12");
            }
            if (speler.Locatie == 19)
            {
                //nog te maken bool Herberg
                //MessageBox.Show("Beurt overslaan");
            }
            if (speler.Locatie == 31)
            {
                //nog te maken bool Put
               // MessageBox.Show("Put : Je zit in de put tot er een andere speler in komt");
            }
            if (speler.Locatie == 42)
            {
                speler.Locatie = 39;
               // MessageBox.Show("Doolhof : Ga naar 39");
            }
            if (speler.Locatie == 52)
            {
                //nog te maken bool Gevangenis
              //  MessageBox.Show("Gevangenis : Je zit in de put tot er een andere speler in komt");
            }
            if (speler.Locatie == 58)
            {
                speler.Locatie = 0;
               // MessageBox.Show("Dood : Terug naar begin");
            }

            if (speler.Locatie > 63)
            {
                
                int TijdelijkeLocatie = speler.Locatie.Value - 63;
                speler.Locatie = 63 - TijdelijkeLocatie;
            }

            if (speler.Locatie == 63)
            {
               // MessageBox.Show("Einde : U heeft gewonnen!");
                speler.Locatie = 62;
                //Speler.Score = Speler.Score + 1;
               // NavigationService.Navigate(new Uri(string.Format("/Lobby.xaml"), UriKind.Relative));
            }

            db.SubmitChanges();
            DobbelEnLocatie[1] = speler.Locatie.Value;
            return DobbelEnLocatie;
        }


        public DTO.Player Inloggen(string naam, string wachtwoord)
        {
            List<DTO.Player> playerList = new List<DTO.Player>();
            

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


        public DTO.GameState Gamestate(DTO.Player player)
        {
            DTO.GameState gamestate = new DTO.GameState();

            var spelers = (from s in db.Players
                           where s.Lobby == player.Lobby
                           select new { s.PlayerNaam, s.Locatie, s.PlayerId } );
             

            foreach (var item in spelers)
            {
                gamestate.players.Add(new DTO.Player() {PlayerNaam=item.PlayerNaam, Locatie= item.Locatie.Value, PlayerId=item.PlayerId });
            }

            
            gamestate.turn = gamestate.players.First();

            return gamestate;
        }
    }
}
