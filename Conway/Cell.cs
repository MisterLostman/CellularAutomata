using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace Conway
{
    public class Cell : DependencyObject
    {
        private RelayCommand flipCommand;
        public RelayCommand FlipCommand
        {
            get { return flipCommand; }
            set
            {
                if (flipCommand == null)
                    flipCommand = new RelayCommand(param => Flip());
            }
        }

        public bool IsAlive
        {
            get { return (bool)GetValue(IsAliveProperty); }
            set { SetValue(IsAliveProperty, value); }
        }

        public byte AliveCount { get; set; }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAliveProperty =
            DependencyProperty.Register("IsAlive", typeof(bool), typeof(Cell));

        public Cell(bool living)
        {
            IsAlive = living;
        }

        public void Flip()
        {
            if (IsAlive == true)
                IsAlive = false;
            else
                IsAlive = true;

            Debug.WriteLine("Flip");
        }  
               
    }
}
