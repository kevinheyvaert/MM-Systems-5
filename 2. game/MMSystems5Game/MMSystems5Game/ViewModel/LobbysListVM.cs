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

namespace MMSystems5Game
{
    public class LobbysListVM:BaseViewModel
    {


        public LobbysListVM()
        {
           App.client1.BeschikbareLobbysCompleted +=client1_BeschikbareLobbysCompleted;
        }

        private ObservableCollection<GanzenBordServiceCloud.Lobby> _beschikbarelobbys;
        public ObservableCollection<GanzenBordServiceCloud.Lobby> BeschikbareLobbys
        {
            get { return _beschikbarelobbys; }
            set { _beschikbarelobbys = value; RaisePropChanged("BeschikbareLobbys"); }
        }

       

        public GanzenBordServiceCloud.Lobby InfoLobby { get; set; }
        public GanzenBordServiceCloud.Lobby TemplateBind { get; set; }

        private bool _join { get; set; }
        public bool Join
        {
            get { return _join; }
            set { _join = value; RaisePropChanged("Join"); }
        }
        
       
        void client1_BeschikbareLobbysCompleted(object sender, GanzenBordServiceCloud.BeschikbareLobbysCompletedEventArgs e)
        {


                if (BeschikbareLobbys == null || BeschikbareLobbys.Count != e.Result.Count)
                {
                    BeschikbareLobbys = e.Result;
                }
                if (App.gamestate.Start)
                {
                    App.start.Startclient();
                }
                

           
        }

        public void GetLobbys()
        {
            try
            {
                App.client1.BeschikbareLobbysAsync();
                

            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        
    }
}
