using CatShop.Application;
using CatShop.Domain;
using CatShop.EFData;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
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
        private readonly ObservableCollection<CatVM> _cats;
        private readonly ViewModelCats cat = new ViewModelCats();

        private EFContext _context = new EFContext();
        public AddNewCat(ObservableCollection<CatVM> cats)
        {        
                          
            InitializeComponent();
            cat.EnableValidation = false;
            DataContext = cat;
            _cats = cats;

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
            cat.EnableValidation = true;

            if (string.IsNullOrEmpty(cat.Error))
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
                var cat_info =
                   new AppCat
                   {
                       Name = tbname.Text,
                       Birth = (DateTime)bdcat.SelectedDate,
                       Description = tbdesc.Text,
                       Gender = cbitem.Text.ToString(),
                       Image = file_name
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

                _cats.Add(new CatVM
                {
                    Id = cat_info.Id.ToString(),
                    Name = cat_info.Name,
                    Birthday = cat_info.Birth,
                    Description = cat_info.Description,
                    Image = cat_info.Image

                });

                _context.Add(cat_info);
                _context.SaveChanges();

                Close();
            }
            else
            {
                MessageBox.Show(cat.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //cat.EnableValidation = true;


        }
    }
}
