﻿using CatShop.EFData;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddNewCat.xaml
    /// </summary>
    public partial class AddNewCat : Window
    {
        private EFContext _context = new EFContext();
        public AddNewCat()
        {
            InitializeComponent();
        }

       
    }
}