using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Net.NetworkInformation;

namespace MMSystems5Game
{
   public class InloggenVM : PhoneApplicationPage  
    {
        public InloggenVM()
        {
            try
            {
                App.client1.InloggenCompleted += client1_InloggenCompleted;
            }
            catch (Exception)
            {
                
                throw;
            }
          
        }

         
        public void inloggen(string playernaam, string wachtwoord)
        {
            try
            {
                
                App.client1.InloggenAsync(playernaam, wachtwoord);
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }


       // public event EventHandler<string> NavigateRequested;

        void client1_InloggenCompleted(object sender, GanzenBordServiceCloud.InloggenCompletedEventArgs e)
        {
            if (e.Result != null)
            {

                App.player = e.Result;
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(string.Format("/MainGame.xaml"), UriKind.Relative));
                App.player.IsHost = false;
               
                
            }

            else
            {
                MessageBox.Show("Wachtwoord en of gebruikersnaam is niet juist");
            }


        }



        
    }
}
