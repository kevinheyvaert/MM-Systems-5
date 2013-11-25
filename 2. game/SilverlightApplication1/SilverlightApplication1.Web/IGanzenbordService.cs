using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SilverlightApplication1.Web
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
        DTO.Player JoinLobby(DTO.Lobby lobby, DTO.Player player);

        [OperationContract]
        void ExitLobby(DTO.Player player);

        [OperationContract]
        void StopHost(DTO.Player player);

        [OperationContract]
        void Start(DTO.Lobby lobby);

        [OperationContract]
        DTO.GameState Gamestate(DTO.Player player);
    }
}
