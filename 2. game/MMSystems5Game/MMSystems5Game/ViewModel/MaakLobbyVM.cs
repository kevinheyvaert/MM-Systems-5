using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSystems5Game
{
    public class MaakLobbyVM:BaseViewModel
    {
        public MaakLobbyVM()
        {
            App.client1.MaakLobbyCompleted += client1_MaakLobbyCompleted;
            createstop = "Create lobby"; // voor als de speler geen lobby heeft. IsHost geeft  geen propertychanged event hier bij false omdat deze al op false staat
        
        }
        private string _createstop;
        public string createstop
        {
            get { return _createstop; }
            set { _createstop = value; RaisePropChanged("createstop"); }
        }

        void client1_MaakLobbyCompleted(object sender, GanzenBordServiceCloud.MaakLobbyCompletedEventArgs e)
        {
            if(e.Result!=null)
            {
                  App.player.Lobby = App.player.PlayerNaam;
                  App.player.IsHost = true;
                    
            }
        }

        public void MaakLobby(GanzenBordServiceCloud.Player player)
        {
            try
            {
                App.client1.MaakLobbyAsync(player);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
    }
}
