using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MMSystems5Silverlight.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGanzenbordService" in both code and config file together.
    [ServiceContract]
    public interface IGanzenbordService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<int> Gooi(DTO.Player player);

        [OperationContract]
        DTO.Player Inloggen(string naam, string wachtwoord);

        [OperationContract]
        DTO.Player MaakAccount(string PlayerNaam, string Wachtwoord);

        [OperationContract]
        List<DTO.Lobby> BeschikbareLobbys();

        [OperationContract]
        DTO.Player MaakLobby(DTO.Player player);

        [OperationContract]
        List<DTO.Player> LobbyInfo(DTO.Lobby lobby);

        [OperationContract]
        void JoinLobby(DTO.Lobby lobby, DTO.Player player);

        [OperationContract]
        void ExitLobby(DTO.Player player);

        [OperationContract]
        void StopHost(DTO.Player player);

        [OperationContract]
        void Start(DTO.Lobby lobby);

        [OperationContract]
        DTO.GameState Gamestate(DTO.Player player);
        
        
        //[DataContract]
        //public class Bord
        //{
        //    [DataMember]
        //    public int[] plaats;
        //}

        //[DataContract]
        //public class Lobby
        //{
        //    public Lobby(string playername)
        //    {
        //        HostPlayer = new Player() { UserName = playername };
        //        Players = new List<Player>();
        //        Players.Add(HostPlayer);
        //        this.LobbyName = playername;
        //        WachtOpPlayers = true;

        //    }

        //    [DataMember]
        //    public string LobbyName { get; set; }
        //    [DataMember]
        //    public List<Player> Players { get; set; }
        //    [DataMember]
        //    public Player HostPlayer { get; private set; }
        //    [DataMember]
        //    public bool WachtOpPlayers
        //    {
        //        get {return WachtOpPlayers}
        //        set
        //        {
        //            if (Players.Count > 4)
        //            {
        //                WachtOpPlayers = false;
        //            }
        //        }
        //    }

        //}

        //public void UpdateLobby()
        //{


        //}

        //private static List<Lobby> gameLobbies = new List<Lobby>();

        //[OperationContract]
        //public IEnumerable<Lobby> GetLobbies()
        //{
        //    var waiting = from p in gameLobbies where p.WachtOpPlayers select p;
        //    return waiting;
        //}

        //[OperationContract]
        //public bool CreateLobby(string playername)
        //{
        //    Lobby lob = new Lobby(playername);
        //    gameLobbies.Add(lob);
        //    return true;
        //}

        //[OperationContract]
        //public bool EnterLobby(string playername)         
        //  {             
        //      if (playername != null)             
        //  {                  
        //          var lob =from p in gameLobbies where playername == p.LobbyName select p).FirstOrDefault();                 
        //              lob.Players.Add(new Player() {UserName = playername});                     
        //              return true;                 

        //      }                        
        //      return false;         
        //  } 
    }
}
