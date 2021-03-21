using System;
using System.Collections.Generic;
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
        CatVM cat = new CatVM();
        public MainWindow()
        {
            InitializeComponent();
            cat.PropertyChanged += Cat_PropertyChanged;
        }

        private void Cat_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            cat.Name = "Murrr";
        }
    }
}
