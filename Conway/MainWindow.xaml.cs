using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Conway
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board board;
        System.Windows.Threading.DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            board = new Board();                             
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Cell cell = (Cell)button.DataContext;
            

            cell.Flip();
            //Debug.WriteLine(board.AliveCount(cell));
        }
        
        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            board.Update();
        }

        private void CreateBoard(object sender, RoutedEventArgs e)
        {
            board.CreateNewBoard(80,80);
            conwayGrid.ItemsSource = board.cellBoard;       
        }

        private void NeighborFlip(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Cell cell = (Cell)button.DataContext;

            foreach (Cell neighbor in board.neighborDict[cell])
            {
                neighbor.Flip();
            }
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            foreach (Cell cell in board.cellBoard)
                cell.IsAlive = false;
        }

        private void BorderToggle(object sender, RoutedEventArgs e)
        {
            var config = (Configuration)mainWindow.FindResource("config");
            if (config.BorderOpacity == 1)
                config.BorderOpacity = 0;
            else
                config.BorderOpacity = 1;

            Debug.WriteLine(config.BorderOpacity);
        }

        private void RunSimulation(object source, RoutedEventArgs e)
        {
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Update;
            timer.Start();
        }

        private void StopSimulation(object source, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void Update(object sender, EventArgs e)
        {
            board.Update();
        }
    }
}
