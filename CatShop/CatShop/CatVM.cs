﻿using System;
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
        private DateTime _birth;
        private int _price;
        private string _description;
        private string _image;


        public string Name
        {
            get { return _name; }
            set 
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public DateTime Birthday
        {
            get { return _birth; }
            set
            {
                _birth = value;
                NotifyPropertyChanged("Birthday");
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyPropertyChanged("Price");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                NotifyPropertyChanged("Image");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string property_name)
        {          
              PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property_name));
        }
    }
}
