using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CatShop.Application
{
    public class CatVM : INotifyPropertyChanged
    {
               
      
       
        private string _id;
        private string _name;
        private DateTime _birth;
        private decimal _price;
        private string _description;
        private string _gender;
        private string _image;
      
                     
       
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;               
               NotifyPropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;               
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public DateTime Birthday
        {
            get { return _birth; }
            set
            {
                _birth = value;                
                NotifyPropertyChanged(nameof(Birthday));
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;               
                NotifyPropertyChanged(nameof(Price));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;               
                 NotifyPropertyChanged(nameof(Description));
            }
        }

        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;                
                NotifyPropertyChanged(nameof(Gender));
            }
        }

        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;               
                NotifyPropertyChanged(nameof(Image));
            }
        }
              
        

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string property_name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property_name));
        }



        


        

    }
}
