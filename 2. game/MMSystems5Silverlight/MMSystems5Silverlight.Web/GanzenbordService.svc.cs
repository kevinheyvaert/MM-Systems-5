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
        List<int> DobbelEnLocatie;

        private int playerid;

        public GanzenbordService()
        {


            Dobbelsteen1 = new Dobbelsteen();
            db = new GanzenBordCloudSqlDataContext();


        }
        public void DoWork()
        {
        }

        public List<int> Gooi(DTO.Player player)
        {

            // list for location and for dice number
            DobbelEnLocatie = new List<int>();
            // class for generate a number between 1 and 6
            DobbelEnLocatie.Add(Dobbelsteen1.GeefWaardeDobbelsteen());
            try
            {   // update location + rules
                var speler = (from s in db.Players
                              where s.PlayerId == player.PlayerId
                              select s).First();
                
                    
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
                    else if (speler.Locatie == 19)
                    {
                        //nog te maken bool Herberg
                        //MessageBox.Show("Beurt overslaan");
                    }
                    else if (speler.Locatie == 31)
                    {
                        //nog te maken bool Put
                        // MessageBox.Show("Put : Je zit in de put tot er een andere speler in komt");
                    }
                    else if (speler.Locatie == 42)
                    {
                        speler.Locatie = 39;
                        // MessageBox.Show("Doolhof : Ga naar 39");
                    }
                    else if (speler.Locatie == 52)
                    {
                        //nog te maken bool Gevangenis
                        //  MessageBox.Show("Gevangenis : Je zit in de put tot er een andere speler in komt");
                    }
                    else if (speler.Locatie == 58)
                    {
                        speler.Locatie = 0;
                        // MessageBox.Show("Dood : Terug naar begin");
                    }

                    else if (speler.Locatie > 63)
                    {

                        int TijdelijkeLocatie = speler.Locatie - 63;
                        speler.Locatie = 63 - TijdelijkeLocatie;
                    }

                    else if (speler.Locatie == 63)
                    {
                        // MessageBox.Show("Einde : U heeft gewonnen!");
                        speler.Locatie = 62;
                        //Speler.Score = Speler.Score + 1;
                        // NavigationService.Navigate(new Uri(string.Format("/Lobby.xaml"), UriKind.Relative));
                    }

                    DobbelEnLocatie.Add(speler.Locatie);
                    db.SubmitChanges();



                    if (DobbelEnLocatie[0] != 6)
                    {
                        next(player);
                    }
                return DobbelEnLocatie;

            }
            catch (Exception)
            {

                DobbelEnLocatie.Add(0);
                return DobbelEnLocatie;
            }
        }


        public DTO.Player Inloggen(string naam, string wachtwoord)
        {
            try
            {

               // List<DTO.Player> playerList = new List<DTO.Player>();

                DTO.Player player = new DTO.Player();

                // look for player
                //var usercontrol = from u in db.Players
                //                  where u.PlayerNaam == naam && u.Wachtwoord == wachtwoord
                //                  select new { u.PlayerNaam, u.Gewonnen, u.Verloren, u.Wachtwoord, u.PlayerId, u.Lobby, u.IsHost };

                var user = (from u in db.Players
                            where u.PlayerNaam == naam && u.Wachtwoord == wachtwoord
                            select u).First();

                player.PlayerId = user.PlayerId;
                player.PlayerNaam = user.PlayerNaam;
                player.Lobby = user.Lobby;

                
                     player.Locatie = user.Locatie;
                
                if (user.IsHost.HasValue)
                   player.IsHost = user.IsHost.Value;
                

                if (user.HostID.HasValue)
                    player.HostID = user.HostID.Value;

                
                if (user.Gewonnen.HasValue)
                    player.Gewonnen = user.Gewonnen.Value;
                

                if (user.Verloren.HasValue)
                    player.Verloren = user.Verloren.Value;
                
                
                



                // add players attributes  
                //foreach (var item in user)
                //{
                //    playerList.Add(new DTO.Player() { PlayerNaam = item.PlayerNaam, Wachtwoord = item.Wachtwoord, PlayerId = item.PlayerId, Lobby = item.Lobby, IsHost=item.IsHost.Value, Gewonnen=item.Gewonnen.Value, Verloren=item.Verloren.Value });
                //}
                return player;
            }

            catch (Exception)
            {

                throw;
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
            try
            {

            // look for joinable lobbys
            var BeschikbareLobbys = (from l in db.Lobbis
                                     where l.CanJoinLobby == true
                                     select new { l.HostPlayer,l.HostID });

            List<DTO.Lobby> LobbyList = new List<DTO.Lobby>();

            foreach (var item in BeschikbareLobbys)
            {
                LobbyList.Add(new DTO.Lobby() { HostPlayer = item.HostPlayer, HostID=item.HostID });
            }

            return LobbyList;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public DTO.Player MaakLobby(DTO.Player player)
        {
            try
            {

          
           
                Lobbi lobby = new Lobbi();
                lobby.HostPlayer = player.PlayerNaam;
                lobby.CanJoinLobby = true;
                lobby.AantalPlayers = 1;
                lobby.HostID = player.PlayerId;
               
                db.Lobbis.InsertOnSubmit(lobby);
                db.SubmitChanges();

                var isHost = (from i in db.Players
                              where i.PlayerId == player.PlayerId
                              select i).FirstOrDefault();

                isHost.Lobby = player.PlayerNaam;
                isHost.IsHost = true;
                isHost.HostID = player.PlayerId;
                db.SubmitChanges();
                if (player.Lobby != null)
                {
                    updatelobby(player.HostID);
                }
                player.HostID = player.PlayerId;
                player.Lobby = player.PlayerNaam;
                player.IsHost = true;
                return player;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<DTO.Player> LobbyInfo(DTO.Lobby lobby)
        {
            try
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
            catch (Exception)
            {

                throw;
            }
            
           

        }

        private void updatelobby(int lobby)
        {
            try
            {
                
                var ad = (from i in db.Lobbis
                          where i.HostID == lobby
                          select i).First();

                var count = (from i in db.Players
                             where i.HostID == lobby
                             select i).Count();


                // update for player count in lobby
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
            catch (Exception)
            {
                
                throw;
            }

           

        }

        private void updateaantal(int lobby, bool canjoin)
        {

            try
            {
                var ad = (from i in db.Lobbis
                          where i.HostID == lobby
                          select i).First();

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
            catch (Exception)
            {
                
                throw;
            }
           

        }

        public DTO.Player JoinLobby(DTO.Lobby lobby, DTO.Player player)
        {
            DTO.Player returnplay = new DTO.Player();
            try
            {
                if (player.IsHost == true)
                {
                    StopHost(player);
                }

                var join = (from l in db.Players
                            where l.PlayerId == player.PlayerId
                            select l).First();

                var isthere = (from l in db.Lobbis
                               where l.HostID== lobby.HostID
                               select l);

                if(isthere!=null )
                {
                    join.Lobby = lobby.HostPlayer;
                    join.HostID = lobby.HostID;
                    db.SubmitChanges();
                    updatelobby(lobby.HostID);
                }

                returnplay = player;
                returnplay.HostID = lobby.HostID;
                returnplay.Lobby = lobby.HostPlayer;
                return returnplay;
            }

            catch (Exception)
            {
                
                throw;
            }
           

        }


        public void ExitLobby(DTO.Player player)
        {
            try
            {
                var exit = (from l in db.Players
                            where l.HostID == player.HostID && l.PlayerId == player.PlayerId
                            select l).First();

                exit.Lobby = null;
                exit.HostID = null;
                exit.Locatie = 0;
                db.SubmitChanges();
                updatelobby(player.HostID);
            }
            catch (Exception)
            {
                
                throw;
            }
          

        }


        public void StopHost(DTO.Player player)
        {
            try
            {
                var stophost = (from s in db.Players
                                where s.HostID == player.PlayerId
                                select s);

                foreach (var item in stophost)
                {
                    item.Lobby = null;
                    item.IsHost = false;
                    item.HostID = null;
                    item.Locatie = 0;
                   
                }

                var stoplobby = (from s in db.Lobbis
                                 where s.HostID == player.PlayerId
                                 select s).First();

                db.Lobbis.DeleteOnSubmit(stoplobby);
                db.SubmitChanges();
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        public void Start(DTO.Lobby lobby)
        {
            var lob = (from l in db.Lobbis
                       where l.HostID == lobby.HostID
                       select l).FirstOrDefault();

            lob.Start = true;
            lob.CanJoinLobby = false;
            db.SubmitChanges();
        }

        private void next(DTO.Player player)
        {
            var lob = (from l in db.Lobbis
                       where l.HostID == player.HostID
                       select l).FirstOrDefault();
           
            var players = (from p in db.Players
                          where p.HostID == lob.HostID
                          select p).Count();

            
            if (lob.WhosTunrId.HasValue)
            {
                int turn = lob.WhosTunrId.Value;
                if (turn == players)
                    lob.WhosTunrId = 0;
                else
                    lob.WhosTunrId++;
            }
            db.SubmitChanges();
        }


        public DTO.GameState Gamestate(DTO.Player player)
        {
            DTO.GameState gamestate = new DTO.GameState();
            
            try
            {

                var spelers = (from s in db.Players
                               where s.HostID == player.HostID
                               select s);

                if (spelers != null)
                {

                    gamestate.players=new List<DTO.Player>();
                    foreach (var item in spelers)
                    {
                       
                        gamestate.players.Add(new DTO.Player() { PlayerNaam = item.PlayerNaam, Locatie = item.Locatie, PlayerId = item.PlayerId });
                    }
                }

               
                var lobby = (from l in db.Lobbis
                             where l.HostID == player.HostID
                             select l).First();


                if (lobby != null)
                {
                    if (lobby.Start!=null)
                    {
                        gamestate.Start = lobby.Start.Value;
                    }

                    if (lobby.WhosTunrId!=null)
                    {
                        gamestate.turn = gamestate.players[lobby.WhosTunrId.Value];
                    }

                    
                }

                else
                {
                    
                    gamestate.Start = false;
                    
                }
               
                return gamestate;

            }
            catch (Exception)
            {
                throw;
            }
        }

        }


}
