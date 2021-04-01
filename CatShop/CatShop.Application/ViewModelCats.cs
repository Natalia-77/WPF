using System;
using System.ComponentModel;
using System.Linq;


namespace CatShop.Application
{
    public class ViewModelCats : INotifyPropertyChanged, IDataErrorInfo
    {      

       
        private string _id;
        private string _name;
        private DateTime _birth;
        private decimal _price;
        private string _description;
        private string _gender;
        private string _image;

        public bool EnableValidation { get; set; }
        private readonly CatValidator _catvalid;

        public ViewModelCats()
        {
            _catvalid = new CatValidator();
        }

        #region Property
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public DateTime Birthday
        {
            get { return _birth; }
            set
            {
                _birth = value;
                OnPropertyChanged("Birthday");

            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");

            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");

            }
        }

        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged("Gender");

            }
        }

        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");

            }
        }
        #endregion

        #region Validation


        //public string this[string name]
        //{
        //    get
        //    {
        //======test==============

        //string result = null;               


        //    switch (name)
        //    {
        //        case "Price": if (Price <= 0) result = "Incorrect price!"; break;
        //        case "Name": if (string.IsNullOrEmpty(Name)) result = "Name could not be empty!"; break;
        //        case "Birthday": if (Birthday > DateTime.Now) result = "Not valid date!"; break;
        //    };

        //return result;

        //=====test===============

        //        return GetValid(name);

        //    }
        //}

        //public string GetValid(string names)
        //{
        //    string result = null;


        //    switch (names)
        //    {
        //        case "Price": if (Price <= 0) result = "Incorrect price!"; break;
        //        case "Name": if (string.IsNullOrEmpty(Name)) result = "Name could not be empty!"; break;
        //        case "Birthday": if (Birthday > DateTime.Now) result = "Not valid date!"; break;
        //    };

        //    return result;
        //}
        //public bool IsValidName
        //{
        //    get
        //    {
        /// foreach (string prop in ValidationErrors)

        //        if (!string.IsNullOrEmpty(GetValid("Name")))
        //            return false;

        //        else
        //        {
        //            return true;
        //        }

        //    }
        //}
        #endregion

        #region ErrorDataInfo members

        public string this[string columnName]
        {
            get
            {
                if (EnableValidation)
                {
                    var firstOrDefault = _catvalid.Validate(this)
                        .Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                    if (firstOrDefault != null)
                        return _catvalid != null ? firstOrDefault.ErrorMessage : "";
                }
                return "";
            }
        }
        public string Error
        {
            get
            {
                if (_catvalid != null)
                {
                    if (EnableValidation)
                    {
                        var results = _catvalid.Validate(this);
                        if (results != null && results.Errors.Any())
                        {
                            var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
                            return errors;
                        }
                    }
                }
                return string.Empty;
            }
        }



        //static readonly string[] ValidationErrors = { "Name", "Price", "Birthday" };

        //public string Error
        //{ 
        //    get
        //    {                              
        //        return null;
        //    }
        //}



        #endregion

        #region Implementtation interface

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


    }
}
