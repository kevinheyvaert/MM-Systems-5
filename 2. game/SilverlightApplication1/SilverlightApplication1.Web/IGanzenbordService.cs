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
        int Gooi();

        [OperationContract]
        DTO.Player Inloggen(string naam, string wachtwoord);

        [OperationContract]
        DTO.Player MaakAccount(string naam, string wachtwoord);

        [OperationContract]
        List<DTO.Lobby> BeschikbareLobbys();
       
        [OperationContract]
        DTO.Player MaakLobby(DTO.Player player);
    }
}
