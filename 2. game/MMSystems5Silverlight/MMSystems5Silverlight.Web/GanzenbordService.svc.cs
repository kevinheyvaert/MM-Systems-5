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
                speler.Diced=DobbelEnLocatie[0];


                if (speler.Locatie == 6)
                    speler.Locatie = 12;
  
                else if (speler.Locatie == 19)
                    speler.Rule_19 = true;
                
                else if (speler.Locatie == 31)
                    speler.Rule_32 = true;
                
                else if (speler.Locatie == 42)
                    speler.Locatie = 39;
                
                else if (speler.Locatie == 52)
                    speler.Rule_52 = true;
                
                else if (speler.Locatie == 58)
                    speler.Locatie = 0;
                

                else if (speler.Locatie > 63)
                {
                    int TijdelijkeLocatie = speler.Locatie - 63;
                    speler.Locatie = 63 - TijdelijkeLocatie;
                }

                else if (speler.Locatie == 63)
                     Einde(player); 
                
               

                if (DobbelEnLocatie[0] != 6)
                    next(player);

                DobbelEnLocatie.Add(speler.Locatie);
                db.SubmitChanges();

                return DobbelEnLocatie;

            }
            catch (Exception)
            {
                
                return null;
            }
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
                    speler.Rule_19 = false;
                    speler.Rule_52 = false;
                }
                else
                {
                    item.Verloren++;
                    item.Locatie = 0;
                    item.Rule_52 = false;
                    item.Rule_19 = false;
                }
                

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

                DTO.Player player = new DTO.Player();
                
                var user = (from u in db.Players
                            where u.PlayerNaam == naam && u.Wachtwoord == wachtwoord
                            select u).First();

                if (user != null)
                {
                    player.PlayerId = user.PlayerId;
                    player.PlayerNaam = user.PlayerNaam;
                    player.Lobby = user.Lobby;
                    if (user.HostID.HasValue)
                    {
                        player.HostID = user.HostID.Value;
                    }

                    player.Locatie = user.Locatie;
                    StopHost(player);
                    if (user.Gewonnen.HasValue)
                    {
                        player.Gewonnen = user.Gewonnen.Value;
                    }

                    if (user.Verloren.HasValue)
                    {
                        player.Verloren = user.Verloren.Value;
                    }

                    user.Locatie = 0;
                    user.Rule_19 = false;
                    user.Rule_52 = false;
                    user.IsHost = false;

                    db.SubmitChanges();
                    player.IsHost = false;
                    return player;
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
                player.Locatie = 0;
                player.Rule_19 = false;
                player.Rule_52 = false;
                player.Rule_32 = false;
                player.Diced = 0;
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
                return null;
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
            List<DTO.Player> lobbyinfo = new List<DTO.Player>();
            try
            {
               
                var spelersinlobby = (from s in db.Players
                                      where s.Lobby == lobby.HostPlayer
                                      select new { s.PlayerNaam });
                //geeft een lijst van spelers in een lobby
                
                foreach (var item in spelersinlobby)
                {
                    lobbyinfo.Add(new DTO.Player() { PlayerNaam = item.PlayerNaam });
                }

                return lobbyinfo;
            }
            catch (Exception)
            {
                return null;
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
                    join.Locatie = 0;
                    join.Diced = 0;
                    join.Rule_19 = false;
                    join.Rule_32 = false;
                    join.Rule_52 = false;

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
                return null;
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
                exit.Rule_32 = false;
                exit.Diced = 0;
                db.SubmitChanges();
                updatelobby(player.HostID);
            }
            catch (Exception)
            {
                
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
                    item.Rule_32 = false;
                    item.Diced = 0;
                    
                }
                // lobby wordt gewist uit de database
                var stoplobby = (from s in db.Lobbis
                                 where s.HostID == player.PlayerId
                                 select s).First();

                if (stoplobby!=null)
                {
                    db.Lobbis.DeleteOnSubmit(stoplobby);
                }
               
                db.SubmitChanges();
            }
            catch (Exception)
            {

            }
        }

        public void Start(DTO.Lobby lobby)
        {
            var Speler = (from s in db.Players
                          where s.HostID == lobby.HostID
                          select s);
           
            foreach (var item in Speler)
            {
                item.Locatie = 0;
            }
            db.SubmitChanges();
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

            if (lobby.WhosTunrId.HasValue)
            {
                int turn = lobby.WhosTunrId.Value;
                players = players - 1;

                if (turn == players)
                    lobby.WhosTunrId = 0;
                else
                    lobby.WhosTunrId++;
                //wordt bij elke beurt gekeken of regel 19,31 of 52 van kracht is
                while (state.players[lobby.WhosTunrId.Value].Rule_19 || state.players[lobby.WhosTunrId.Value].Rule_52 || state.players[lobby.WhosTunrId.Value].Rule_31)
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
                    if (state.players[lobby.WhosTunrId.Value].Rule_52 || state.players[lobby.WhosTunrId.Value].Rule_31)
                    {
                        foreach (var item in state.players)
                        {
                            if (state.players[lobby.WhosTunrId.Value].Locatie == item.Locatie && item.PlayerId != state.players[lobby.WhosTunrId.Value].PlayerId)
                            {
                                var speler = (from s in db.Players
                                              where s.PlayerId == state.players[lobby.WhosTunrId.Value].PlayerId
                                              select s).First();
                               
                                if (state.players[lobby.WhosTunrId.Value].Rule_31)
                                {
                                    speler.Rule_32 = false;
                                    state.players[lobby.WhosTunrId.Value].Rule_31 = false;
                                }

                                else if (state.players[lobby.WhosTunrId.Value].Rule_52)
                                {
                                    speler.Rule_52 = false;
                                    state.players[lobby.WhosTunrId.Value].Rule_52 = false;
                                }
                            }
                        }
                        if (lobby.WhosTunrId == players)
                            lobby.WhosTunrId = 0;
                        else
                            lobby.WhosTunrId++;
                    }

                }
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
                        gamestate.players.Add(new DTO.Player() { PlayerNaam = item.PlayerNaam, Locatie = item.Locatie, PlayerId = item.PlayerId, Rule_19 = item.Rule_19.Value, Rule_52 = item.Rule_52.Value, Rule_31=item.Rule_32.Value,Diced=item.Diced.Value});
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
                return null;
            }
        }

        public List<DTO.Player> HighScore()
        {
            List<DTO.Player> Spelers = new List<DTO.Player>();
            var players = (from p in db.Players
                           orderby  p.Gewonnen descending
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

