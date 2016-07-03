using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conway
{
    public class Configuration : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
