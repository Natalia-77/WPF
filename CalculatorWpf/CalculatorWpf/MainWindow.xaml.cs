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

namespace CalculatorWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();           
            //wrap_panel.IsEnabled = false;
            //tb_boxres.IsEnabled = false;           
        }

        private void btn_buttons_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            tb_boxres.Text += button.Content;
        }

        private void btnOn_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnOn)
            {
                tb_boxres.Text = "";
                //wrap_panel.IsEnabled = true;
                //tb_boxres.IsEnabled = true;
                //MessageBox.Show("+");
            }

        }
    }
}
