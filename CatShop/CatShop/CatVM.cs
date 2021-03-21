using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatShop
{
    public class CatVM : INotifyPropertyChanged
    {

        private string _name;

        public string Name
        {
            get { return _name; }
            set 
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string property_name)
        {          
              PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property_name));
        }
    }
}
