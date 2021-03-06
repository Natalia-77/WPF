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

namespace WpfResource
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ///Resources["backcolor"] = new SolidColorBrush(Colors.Red);
            ///Якщо назва ресурсу неправильна.
            Button cmd = (Button)sender;
            try
            {               
                SolidColorBrush brush = (SolidColorBrush)cmd.FindResource("TileBrush");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }          

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            try
            {                
                Resources["backcolor"] = new SolidColorBrush(Colors.Red);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
