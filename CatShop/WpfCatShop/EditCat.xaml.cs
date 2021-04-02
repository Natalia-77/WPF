using CatShop.Application;
using CatShop.Domain;
using CatShop.EFData;
using FluentValidation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
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
        private readonly ViewModelCats cat = new ViewModelCats();
        private readonly CatValidator _catvalid = new CatValidator();

        public EditCat(int res)            
        {
            InitializeComponent();
            cat.EnableValidation = true;
            _res = res;
            DataContext = cat;
            
        }       

        private void Button_Click(object sender, RoutedEventArgs e)
        {                
            var cat_item = _context.Cats
              .SingleOrDefault(p => p.Id ==_res);
           

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
                cat_item.Image = fileSavePath;
                _context.SaveChanges();
            }


            if (!string.IsNullOrEmpty(tbnewname.Text))
            {
                if (!cat.Error.Equals("Недопустимі цифри і латиниця!"))
                {
                     //MessageBox.Show("true");
                    cat_item.Name = tbnewname.Text;
                    _context.SaveChanges();
                }

            }

            if (!string.IsNullOrEmpty(tbprice.Text))
            {
                if (!cat.Error.Equals("Повинно бути більше нуля!"))
                {
                    //MessageBox.Show("+++++");
                    cat_item.AppCatPrices = new List<AppCatPrice>
                    {
                      new AppCatPrice
                      {
                          CatId=cat_item.Id,
                          DateCreate=DateTime.Now,
                          Price=decimal.Parse(tbprice.Text)
                      }

                    };
                    _context.SaveChanges();
                }

            }
            //dpdatebirth
            if (dpdatebirth.SelectedDate != null)
            {
                if (!cat.Error.Equals("Некоректна дата!"))
                {
                    cat_item.Birth = (DateTime)dpdatebirth.SelectedDate;
                    _context.SaveChanges();
                }
            }


            if (!string.IsNullOrEmpty(tbdes.Text))
            {
                cat_item.Description = tbdes.Text;
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
