using CatShop.Application;
using CatShop.EFData;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
using WpfCatShop.Helper;

namespace WpfCatShop
{
    /// <summary>
    /// Interaction logic for EditCat.xaml
    /// </summary>
    public partial class EditCat : Window
    {
        private EFContext _context = new EFContext();        
        public int _res { get; set; }
        private string file_select = string.Empty;

        public EditCat(int res)            
        {
            InitializeComponent();
            _res = res;
        }       

        private void Button_Click(object sender, RoutedEventArgs e)
        {                
            var post = _context.Cats
              .SingleOrDefault(p => p.Id ==_res);
            //MessageBox.Show($"{post.Name}");

            //якщо обрали файл з фото кота.
            if (!string.IsNullOrEmpty(file_select))
            {
                //розширення файла.
                string ext = System.IO.Path.GetExtension(file_select);

                //імя з розширенням файла.
                string fileName = System.IO.Path.GetRandomFileName() + ext;

                //шлях до файла повний.
                string fileSavePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(),
                 "images", fileName);

                //змінюємо розмір фото.
                var bmp = ResizeImage.ResizeOrigImg(
                  new Bitmap(System.Drawing.Image.FromFile(file_select)), 75, 75);

                //зберігаємо фото з вже вказаним розміром.
                bmp.Save(fileSavePath, ImageFormat.Jpeg);

                //заносимо в базу нове фото.
                post.Image = fileSavePath;
                _context.SaveChanges();
            }

            if (!string.IsNullOrEmpty(tbdes.Text))
            {
                post.Description = tbdes.Text;
                _context.SaveChanges();
            }
                    
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;" +
                "*.PNG|All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                try
                {                   
                    file_select = dlg.FileName;
                }
                catch
                {
                    MessageBox.Show("Неможливо відкрити!");
                }
            }

            
        }
    }
}
