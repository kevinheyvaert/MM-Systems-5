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

                if (speler.Locatie == 6)
                {
                    brug(player);

                }
                else if (speler.Locatie == 19)
                {
                    herberg(player);
                }
                else if (speler.Locatie == 31)
                {
                    //nog te maken bool Put
                    // MessageBox.Show("Put : Je zit in de put tot er een andere speler in komt");
                }
                else if (speler.Locatie == 42)
                {
                    doornstruik(player);
                }
                else if (speler.Locatie == 52)
                {
                    jail(player);
                }
                else if (speler.Locatie == 58)
                {
                    dead(player);
                }

                else if (speler.Locatie > 63)
                {

                    int TijdelijkeLocatie = speler.Locatie - 63;
                    speler.Locatie = 63 - TijdelijkeLocatie;
                }

                else if (speler.Locatie == 63)
                {
                    Einde(player); 
                }

                DobbelEnLocatie.Add(speler.Locatie);
                db.SubmitChanges();

                // Als speler geen 6 heeft gegooid, mag de volgende speler beginnen.
                // Als de speler 6 gooit, blijft deze aan de beurt.
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

        private void plaatsgame(DTO.Player player)
        {
            // Voor welke dient deze? zie het niet goed 
            int plaatsingame = 0;
            var lijstspelers = from l in db.Players
                               where l.HostID == player.HostID
                               orderby l.Locatie
                               select l;

            foreach (var item in lijstspelers)
            {
                item.PlaceGame = plaatsingame;
                plaatsingame++;
                db.SubmitChanges();
            }
        }

        private void brug(DTO.Player player)
        {
            // Wanneer speler op locatie 6 komt --> spring naar locatie 12
            var speler = (from s in db.Players
                          where s.PlayerId == player.PlayerId
                          select s).First();
            speler.Locatie = 12;
            db.SubmitChanges();

        }

        private void herberg(DTO.Player player)
        {
            // Wanneer speler op locatie 19 komt --> beurt overslaan
            var speler = (from s in db.Players
                          where s.PlayerId == player.PlayerId
                          select s).First();
            speler.Rule_19 = true;
            db.SubmitChanges();

        }

        private void put(DTO.Player player)
        {
            // Wanneer een speler op locatie 31 komt --> speler blijft in put zitten tot er een andere passeert
            var speler = (from s in db.Players
                          where s.PlayerId == player.PlayerId
                          select s).First();
            //moet nog aangepast worden??
        }
         
        private void doornstruik(DTO.Player player)
        {
            // Wanneer een speler op locatie 42 komt --> terug naar 39
            var speler = (from s in db.Players
                          where s.PlayerId == player.PlayerId
                          select s).First();

            speler.Locatie = 39;
            db.SubmitChanges();
        }

        private void jail(DTO.Player player)
        {
            // Wanneer een speler op locatie 52 komt --> gevangenis : ering blijven tot andere speler op 52 komt
            var lijstspelers = from l in db.Players
                               where l.HostID == player.HostID
                               orderby l.Locatie
                               select l;

            var speler = (from s in db.Players
                          where s.PlayerId == player.PlayerId
                          select s).First();
            speler.Rule_52 = true;
            db.SubmitChanges();
        }

        private void dead(DTO.Player player)
        {
            // Wanneer een speler op locatie 58 komt --> terug naar 0
            var speler = (from s in db.Players
                          where s.PlayerId == player.PlayerId
                          select s).First();

            speler.Locatie = 0;
            db.SubmitChanges();
        }

        private void Einde(DTO.Player player)
        {
            //Einde van het spel
            var lijstspelers = from l in db.Players
                               where l.HostID == player.HostID
                               select l;

            var speler = (from s in db.Players
                          where s.PlayerId == player.PlayerId
                          select s).First();


            foreach (var item in lijstspelers)
            {
                if (speler.PlayerId == item.PlayerId)
                {
                    speler.Gewonnen++;
                }
                else
                {
                    item.Verloren++;
                }
                db.SubmitChanges();

            }
            var lobby = (from l in db.Lobbis
                         where l.HostID == player.HostID
                         select l).First();
            lobby.Start = false;
            lobby.CanJoinLobby = true;
            db.SubmitChanges();

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
                if (user.Gewonnen.HasValue)
                {
                    player.Gewonnen = user.Gewonnen.Value;
                }

                if (user.Verloren.HasValue)
                {
                    player.Verloren = user.Verloren.Value;
                }
               
                player.IsHost = user.IsHost.Value;
                if (player.IsHost)
                {
                    StopHost(player);
                }
                

                user.Locatie = 0;
                user.Rule_19 = false;
                user.Rule_52 = false;

                db.SubmitChanges();
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
                player.Locatie = 0;
                player.Rule_19 = false;
                player.Rule_52 = false;
                player.Gewonnen = 0;
                player.Verloren = 0;
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
                                         select new { l.HostPlayer, l.HostID });

                List<DTO.Lobby> LobbyList = new List<DTO.Lobby>();
                //maakt een lijst aan met de beschikbare lobby's
                foreach (var item in BeschikbareLobbys)
                {
                    LobbyList.Add(new DTO.Lobby() { HostPlayer = item.HostPlayer, HostID = item.HostID });
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
                //Stop eventuele hosting van een bestaande lobby
                StopHost(player);
                //maak nieuwe lobby
                Lobbi lobby = new Lobbi();
                lobby.HostPlayer = player.PlayerNaam;
                lobby.CanJoinLobby = true;
                lobby.AantalPlayers = 1;
                lobby.HostID = player.PlayerId;
                lobby.WhosTunrId = 0;

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
                return player;
            }
        }

        public List<DTO.Player> LobbyInfo(DTO.Lobby lobby)
        {
            try
            {
                var spelersinlobby = (from s in db.Players
                                      where s.Lobby == lobby.HostPlayer
                                      select new { s.PlayerNaam });
                //geeft een lijst van spelers in een lobby
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

                // update for player count in lobby (aantal spelers in lobby --> max 4 spelers!)
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
            //updaten van het aantal in een lobby + al dan niet meer joinen aan die lobby
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
            //Om speler te laten joinen in een lobby
            DTO.Player returnplay = new DTO.Player();
            try
            {
                // als een speler een host is van een lobby --> stop lobby
                if (player.IsHost == true)
                {
                    StopHost(player);
                }

                var join = (from l in db.Players
                            where l.PlayerId == player.PlayerId
                            select l).First();

                var isthere = (from l in db.Lobbis
                               where l.HostID == lobby.HostID
                               select l);

                if (isthere != null)
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
                //Een speler verlaat het spel en de lobby
                exit.Lobby = null;
                exit.HostID = null;
                exit.Locatie = 0;
                exit.Rule_19 = false;
                exit.Rule_52 = false;
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
                //alle spelers die in de lobby zitten die wordt gestopt
                foreach (var item in stophost)
                {
                    item.Lobby = null;
                    item.IsHost = false;
                    item.HostID = null;
                    item.Locatie = 0;
                    item.Rule_19 = false;
                    item.Rule_52 = false;
                    

                }
                // lobby wordt gewist uit de database
                var stoplobby = (from s in db.Lobbis
                                 where s.HostID == player.PlayerId
                                 select s).First();

                db.Lobbis.DeleteOnSubmit(stoplobby);
                db.SubmitChanges();
            }
            catch (Exception)
            {

            }
        }

        public void Start(DTO.Lobby lobby)
        {
            var lob = (from l in db.Lobbis
                       where l.HostID == lobby.HostID
                       select l).FirstOrDefault();
            //spel in de lobby wordt gestart
            lob.Start = true;
            lob.CanJoinLobby = false;
            lob.WhosTunrId = 0;
            db.SubmitChanges();
        }

        private void next(DTO.Player player)
        {
            //bepalen van de volgende beurt van een speler in het spel
            DTO.GameState state = Gamestate(player);

            var lobby = (from l in db.Lobbis
                         where l.HostID == player.HostID
                         select l).First();

            // grote van aantal players in een lobby
            var players = (from p in db.Players
                           where p.HostID == lobby.HostID
                           select p).Count();

            var lijstspeler = (from l in db.Players
                               where l.HostID == player.HostID
                               select l);

            if (lobby.WhosTunrId.HasValue)
            {
                int turn = lobby.WhosTunrId.Value;
                players = players - 1;

                if (turn == players)
                    lobby.WhosTunrId = 0;
                else
                    lobby.WhosTunrId++;
                //wordt bij elke beurt gekeken of regel 19,31 of 52 van kracht is
                while (state.players[lobby.WhosTunrId.Value].Rule_19 || state.players[lobby.WhosTunrId.Value].Rule_52)
                {
                    if (state.players[lobby.WhosTunrId.Value].Rule_19)
                    {
                        var speler = (from s in db.Players
                                      where s.PlayerId == state.players[lobby.WhosTunrId.Value].PlayerId
                                      select s).First();
                        speler.Rule_19 = false;
                        state.players[lobby.WhosTunrId.Value].Rule_19 = false;
                        if (lobby.WhosTunrId == players)
                            lobby.WhosTunrId = 0;
                        else
                            lobby.WhosTunrId++;
                    }
                    if (state.players[lobby.WhosTunrId.Value].Rule_52)
                    {
                        foreach (var item in state.players)
                        {
                            if (state.players[lobby.WhosTunrId.Value].Locatie == item.Locatie && item.PlayerId != state.players[lobby.WhosTunrId.Value].PlayerId)
                            {
                                var speler = (from s in db.Players
                                              where s.PlayerId == state.players[lobby.WhosTunrId.Value].PlayerId
                                              select s).First();
                                speler.Rule_52 = false;
                                state.players[lobby.WhosTunrId.Value].Rule_52 = false;

                            }
                        }
                        if (lobby.WhosTunrId == players)
                            lobby.WhosTunrId = 0;
                        else
                            lobby.WhosTunrId++;

                    }

                }


                // kijken of de volgende player wel het mag zijn.
                //int temp = 0;
                //foreach (var item in lijstspeler)
                //{
                //    if (item.PlayerId == player.PlayerId && temp == lob.WhosTunrId)
                //    {
                //        if (item.Rule_19.HasValue)
                //        {
                //            if (item.Rule_19.Value)
                //            {
                //                if (turn == players)
                //                    lob.WhosTunrId = 0;
                //                else
                //                    lob.WhosTunrId++;

                //                item.Rule_19 = false;
                //            }
                //        }
                //        if (item.Rule_52.HasValue)
                //        {
                //            if (item.Rule_52.Value)
                //            {
                //                foreach (var bitem in lijstspeler)
                //                {
                //                    if (bitem.Locatie == item.Locatie)
                //                    {
                //                        bitem.Rule_52 = false;
                //                    }

                //                    else
                //                    {
                //                        if (turn == players)
                //                            lob.WhosTunrId = 0;
                //                        else
                //                            lob.WhosTunrId++;
                //                    }
                //                }
                //            }
                //        }


                //}
                // temp++;
            }

            db.SubmitChanges();
        }

        public DTO.GameState Gamestate(DTO.Player player)
        {
            DTO.GameState gamestate = new DTO.GameState();
            //Gamestate geeft een lijst met speler weer tijdens het spel
            try
            {
                var spelers = (from s in db.Players
                               where s.HostID == player.HostID
                               select s);

                if (spelers != null)
                {
                    gamestate.players = new List<DTO.Player>();
                    foreach (var item in spelers)
                    {
                        gamestate.players.Add(new DTO.Player() { PlayerNaam = item.PlayerNaam, Locatie = item.Locatie, PlayerId = item.PlayerId, Rule_19 = item.Rule_19.Value, Rule_52 = item.Rule_52.Value });
                    }
                }

                var lobby = (from l in db.Lobbis
                             where l.HostID == player.HostID
                             select l).First();

                if (lobby != null)
                {
                    if (lobby.Start != null)
                    {
                        gamestate.Start = lobby.Start.Value;
                    }
                    if (lobby.WhosTunrId != null)
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

        public List<DTO.Player> HighScore()
        {
            List<DTO.Player> Spelers = new List<DTO.Player>();
            var players = (from p in db.Players
                           orderby  p.Gewonnen
                           select p);

            foreach (var item in players)
            {

                if (item.Gewonnen.Value != 0 || item.Verloren.Value != 0)
                {
                    Spelers.Add(new DTO.Player() { PlayerNaam = item.PlayerNaam, Gewonnen = item.Gewonnen.Value, Verloren = item.Verloren.Value });
                }

            }

            return Spelers;
        }

    }
}

