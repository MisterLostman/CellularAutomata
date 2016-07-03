using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Conway
{
    public class Cell : DependencyObject
    {        
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
        }  
               
    }
}
