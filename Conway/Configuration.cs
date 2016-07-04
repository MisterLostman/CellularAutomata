using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Conway
{
    public class Configuration : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private double borderOpacity = 1;
        public double BorderOpacity
        {
            get { return borderOpacity; }
            set
            {
                borderOpacity = value;
                NotifyPropertyChanged("BorderOpacity");
            }
        }

        
    }
}
