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
        string value_first = "";
        string value_second = "";
        string oper = "";

        public MainWindow()
        {
            InitializeComponent();

        }

        private void btn_buttons_Click(object sender, RoutedEventArgs e)
        {
            int value;
            Button button = (Button)sender;
            string str = (string)((Button)e.OriginalSource).Content;
            tb_boxres.Text += str;
            bool res = int.TryParse(str, out value);
            if (res)
            {
                if (oper == "")
                {
                     value_first += str;
                    //MessageBox.Show(value_first);
                }
                else
                {
                    value_second += str;
                    //MessageBox.Show($"Se +{value_second}");
                }
               
               
            }
            else
            {
                if (str == "=")
                {
                   ResOperation();
                    tb_boxres.Text +=value_second;
                    oper = "";
                }
                else
                {
                    if(value_second!="")
                    {
                        ResOperation();
                        //value_first = value_second;
                        //value_second = "";
                    }
                    oper = str;
                }
            }
        }

        private void btnOn_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnOn)
            {
                tb_boxres.Text = "";
            }

        }

        private void ResOperation()
        {
            int num1 = int.Parse(value_first);
            int num2 = int.Parse(value_second);
          
            switch (oper)
            {
                case "+":
                    {
                        value_second = (num1 + num2).ToString();
                        break;
                    }
                    

            }
        }
    }
}
