using System.Linq;
using System.Text;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;


namespace MMSystems5Game
{
   public class HighScoreVM:BaseViewModel
    {
        private ObservableCollection<GanzenBordServiceCloud.Player> _highscore;
        public ObservableCollection<GanzenBordServiceCloud.Player> HighScore
        {
            get { return _highscore; }
            set { _highscore= value; RaisePropChanged("HighScore"); }
        }

        public void winnaars()
        {
            App.client1.HighScoreAsync();
        }

       
       public HighScoreVM()
       {
           
           App.client1.HighScoreCompleted+=client1_HighScoreCompleted;  
       
       }
       
        void client1_HighScoreCompleted(object sender, GanzenBordServiceCloud.HighScoreCompletedEventArgs e)
        {
            HighScore=e.Result;
        }

        
    }
}
