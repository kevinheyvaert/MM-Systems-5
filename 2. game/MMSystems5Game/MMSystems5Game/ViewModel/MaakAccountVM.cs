
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MMSystems5Game
{
   public class MaakAccountVM:PhoneApplicationPage
    {
        public MaakAccountVM()
        {
            App.client1.MaakAccountCompleted += client1_MaakAccountCompleted;
        }

        void client1_MaakAccountCompleted(object sender, GanzenBordServiceCloud.MaakAccountCompletedEventArgs e)
        {
            App.player = e.Result;
           (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(string.Format("/MainGame.xaml"), UriKind.Relative));
           
        }

        public void navigatielobby()
        {
            App.plaats = true;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(string.Format("/Lobby.xaml"), UriKind.Relative));
        }

        public void MaakAan(string playernaam, string wachtwoord)
        {
            try
            {
                App.client1.MaakAccountAsync(playernaam, wachtwoord);
                App.gametimer.Start();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
    }
}
