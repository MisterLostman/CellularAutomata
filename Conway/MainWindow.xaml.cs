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

        public MainWindow()
        {
            InitializeComponent();
            board = new Board();
            this.DataContext = board;                                  
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
    }
}
