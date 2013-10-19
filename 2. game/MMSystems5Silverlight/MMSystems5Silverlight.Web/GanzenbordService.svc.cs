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
        public void DoWork()
        {
        }
        
        public int  Gooi()
        {   
            
            Dobbelsteen Dobbelsteen1 = new Dobbelsteen();
            return Dobbelsteen1.GeefWaardeDobbelsteen();
        }

        public GanzenBordCloudSqlDataContext gb;
        private int playerid;

        public GanzenbordService()
        {
            gb = new GanzenBordCloudSqlDataContext();

        }

        public DTO.Player Inloggen(string naam, string wachtwoord)
        {
            List<DTO.Player> playerList = new List<DTO.Player>();
            try
            {
                var usercontrol = from u in gb.Players
                                  where u.PlayerNaam == naam && u.Wachtwoord == wachtwoord
                                  select new { u.PlayerNaam, u.Gewonnen, u.Verloren, u.Wachtwoord };

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

        SqlConnection con;
        public void MaakAccount(string PlayerNaam, string Wachtwoord)
        {
            try
            {
                ///using SQL to insert new user
                using (con = new SqlConnection("Server=tcp:hoegfejaux.database.windows.net,1433;Database=GanzenBordDataBase;User ID=KevinDatabase@hoegfejaux;Password=Fiat500R;Trusted_Connection=False;"))
                {
                    try
                    {
                        con.Open();
                    }
                    catch
                    {
                        return;
                    }

                    try
                    {
                        using (SqlCommand command = new SqlCommand(
                        "INSERT INTO Player (Naam, Wachtwoord) VALUES (@Naam, @Wachtwoord)", con))
                        {
                            command.Parameters.Add(new SqlParameter("@PlayerNaam", PlayerNaam));
                            command.Parameters.Add(new SqlParameter("@Wachtwoord", Wachtwoord));
                            command.ExecuteNonQuery();
                            con.Close();
                            con.Dispose();
                        }
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                }


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

                }
                catch (Exception)
                {
                    throw;

                }
            }
            catch
            {

            }
        }
    }
}
