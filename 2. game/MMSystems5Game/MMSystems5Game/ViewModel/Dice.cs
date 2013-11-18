using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MMSystems5Game
{
   public class Dice:BaseViewModel
   {
       
       private Grid _dobbelaantal;
        public Grid DobbelAantal 
        {
            get { return _dobbelaantal; }
            set { _dobbelaantal = value; RaisePropChanged("DobbelAantal"); }
        }
        
        public Dice()
        {
            App.client1.GooiCompleted += client1_GooiCompleted;
        }

        void client1_GooiCompleted(object sender, GanzenBordServiceCloud.GooiCompletedEventArgs e)
        {
         DobbelAantal = dice(e.Result[0]);
        }

        public void dobbel(GanzenBordServiceCloud.Player player)
        {
            App.client1.GooiAsync(player);
        }
        


        //http://cespage.com/silverlight/tutorials/wp7tut7.html
        private void Add(Grid grid, int row, int column)
        {
            Ellipse _dot = new Ellipse();
            _dot.Width = 20;
            _dot.Height = 20;
            _dot.Fill = new SolidColorBrush(Colors.Black);
            _dot.SetValue(Grid.ColumnProperty, column);
            _dot.SetValue(Grid.RowProperty, row);
            grid.Children.Add(_dot);
        }

        private Grid dice(int Value)
        {
            Grid _grid = new Grid();
            _grid.Height = 100;
            _grid.Width = 100;
            for (int index = 0; (index <= 2); index++) // 3 by 3 Grid 
            {
                _grid.RowDefinitions.Add(new RowDefinition());
                _grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            switch (Value)
            {
                case 0:
                    // No Dots
                    break;
                case 1:
                    Add(_grid, 1, 1); // Middle
                    break;
                case 2:
                    Add(_grid, 0, 2); // Top Right
                    Add(_grid, 2, 0); // Bottom Left
                    break;
                case 3:
                    Add(_grid, 0, 2); // Top Right
                    Add(_grid, 1, 1); // Middle
                    Add(_grid, 2, 0); // Bottom Left
                    break;
                case 4:
                    Add(_grid, 0, 0); // Top Left
                    Add(_grid, 0, 2); // Top Right
                    Add(_grid, 2, 0); // Bottom Left
                    Add(_grid, 2, 2); // Bottom Right
                    break;
                case 5:
                    Add(_grid, 0, 0); // Top Left
                    Add(_grid, 0, 2); // Top Right
                    Add(_grid, 1, 1); // Middle
                    Add(_grid, 2, 0); // Bottom Left
                    Add(_grid, 2, 2); // Bottom Right
                    break;
                case 6:
                    Add(_grid, 0, 0); // Top Left
                    Add(_grid, 0, 2); // Top Right
                    Add(_grid, 1, 0); // Middle Left
                    Add(_grid, 1, 2); // Middle Right
                    Add(_grid, 2, 0); // Bottom Left
                    Add(_grid, 2, 2); // Bottom Right
                    break;
                default:
                    break;
            }
            return _grid;
        }
    }


}
