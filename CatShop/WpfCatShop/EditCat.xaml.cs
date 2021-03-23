using CatShop.Application;
using CatShop.EFData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfCatShop
{
    /// <summary>
    /// Interaction logic for EditCat.xaml
    /// </summary>
    public partial class EditCat : Window
    {
        private EFContext _context = new EFContext();        
        public int _res { get; set; }
        public EditCat(int res)
            
        {
            InitializeComponent();
            _res = res;
        }

        public CatVM Item { get; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {                
            var post = _context.Cats
              .SingleOrDefault(p => p.Id ==_res);            
            //MessageBox.Show($"{post.Name}");
            post.Description = tbdes.Text;
            _context.SaveChanges();
            
        }
    }
}
