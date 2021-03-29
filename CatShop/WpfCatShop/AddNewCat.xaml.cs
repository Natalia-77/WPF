using CatShop.Domain;
using CatShop.EFData;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfCatShop.Helper;
using Path = System.IO.Path;

namespace WpfCatShop
{
    /// <summary>
    /// Interaction logic for AddNewCat.xaml
    /// </summary>
    public partial class AddNewCat : Window
    {
        public string file_name { get; set; }
        private string file_selected = string.Empty;
      

        private EFContext _context = new EFContext();
        public AddNewCat()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Bitmap image;
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;" +
                "*.PNG|All files (*.*)|*.*";

            if (dlg.ShowDialog()==true)
            {
                try
                {
                    
                    image = new Bitmap(dlg.FileName);                   
                    file_selected = dlg.FileName;
                }
                catch
                {
                     MessageBox.Show("Неможливо відкрити!");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(file_selected))
            {
                string ext = Path.GetExtension(file_selected);

                string fileName = Path.GetRandomFileName() + ext;

                string fileSavePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "images", fileName);

                var bmp = ResizeImage.ResizeOrigImg(
                    new Bitmap(System.Drawing.Image.FromFile(file_selected)), 75, 75);

                bmp.Save(fileSavePath, ImageFormat.Jpeg);

                file_name = fileSavePath;
               
            }
            var cat_info=
               new AppCat
               {
                   Name=tbname.Text,
                   Birth=(DateTime)bdcat.SelectedDate,
                   Description=tbdesc.Text,
                   Gender=cbitem.Text.ToString(),
                   Image=file_name
               };
            cat_info.AppCatPrices = new List<AppCatPrice>
            {
                new AppCatPrice
                {
                CatId = cat_info.Id,
                DateCreate = DateTime.Now,
                Price = decimal.Parse(tbprice.Text)
                }
            };

            _context.Add(cat_info);
            _context.SaveChanges();
        }

        
    }
}
