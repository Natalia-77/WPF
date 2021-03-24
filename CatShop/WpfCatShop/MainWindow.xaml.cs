using CatShop.Application;
using CatShop.Domain;
using CatShop.EFData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
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

namespace WpfCatShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int _id { get; set; }
        private EFContext _context = new EFContext();
        private ObservableCollection<CatVM> _cats = new ObservableCollection<CatVM>();
        public MainWindow()
        {
            InitializeComponent();
            DBSeeder.SeedData(_context);
        }

        private void Cat_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            new AddNewCat().ShowDialog();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = _context.Cats
               .Select(x => new CatVM()
               {
                   Id=x.Id.ToString(),                   
                   Name = x.Name,
                   Birthday = x.Birth,
                   Description = x.Description,
                   Gender=x.Gender,
                   Image=x.Image
               }).ToList();

          
            _cats = new ObservableCollection<CatVM>(list);            
            dgSimple.ItemsSource = _cats;

        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            Window_Loaded(sender, e);
        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
                      
            if (dgSimple.SelectedItem != null)
            {
                if (dgSimple.SelectedItem is CatVM)
                {
                    var userView = dgSimple.SelectedItem as CatVM;                  
                    int id =int.Parse( userView.Id);
                    _id = id;                   
                }               
            }

            EditCat edit = new EditCat(_id);
            edit.ShowDialog();
        }
    }
}

