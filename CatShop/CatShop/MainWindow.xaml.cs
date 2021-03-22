using Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CatShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private ObservableCollection<CatVM> cats = new ObservableCollection<CatVM>();
        public MainWindow()
        {
            InitializeComponent();           
        }

        private void Cat_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {           

            cats.Add(new CatVM()
            {
                Name = "Мурчик",
                Birthday = new DateTime(2002, 7, 22),
                //Price =3200,
                Description="Чорний окрас,пухнастий хвіст,зелені очі.Кусається,але не царапається...",
                Image = "https://ichef.bbci.co.uk/news/800/cpsprodpb/14236/production/_104368428_gettyimages-543560762.jpg"
            });
            dgSimple.ItemsSource = cats;
        }
    }
}
