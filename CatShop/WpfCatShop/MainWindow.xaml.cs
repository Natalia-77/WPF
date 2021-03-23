using CatShop.Domain;
using CatShop.EFData;
using Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            //_cats.Add(new CatVM()
            //{
            //    Name = "Мурчик",
            //    Birthday = new DateTime(2002, 7, 22),
            //    //Price = 3200,
            //    Description = "Чорний окрас,пухнастий хвіст,зелені очі.Кусається,але не царапається...",
            //    Image = "https://ichef.bbci.co.uk/news/800/cpsprodpb/14236/production/_104368428_gettyimages-543560762.jpg"
            //});
            //dgSimple.ItemsSource = _cats;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = _context.Cats
               .Select(x => new CatVM()
               {
                   Name = x.Name,
                   Birthday = x.Birth,
                   Description = x.Description,
                   Gender=x.Gender,
                   Image=x.Image
               }).ToList();           
            _cats = new ObservableCollection<CatVM>(list);            
            dgSimple.ItemsSource = _cats;
        }
    }
}

