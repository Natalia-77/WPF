using System;
using System.ComponentModel;

namespace CatShop.Application
{
    public class CatVM : INotifyPropertyChanged
    {
        public string _id;
        private string _name;
        private DateTime _birth;
        //private int _price;
        private string _description;
        private string _gender;
        private string _image;

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }
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

        //public int Price
        //{
        //    get { return _price; }
        //    set
        //    {
        //        _price = value;
        //        NotifyPropertyChanged("Price");
        //    }
        //}

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                NotifyPropertyChanged("Gender");
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
