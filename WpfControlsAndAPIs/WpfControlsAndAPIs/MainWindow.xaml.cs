﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlsAndAPIs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            this.inkRadio.IsChecked = true;
            this.comboColors.SelectedIndex = 0;
        }

        private void ColorChanged(object sender, SelectionChangedEventArgs e)
        {




            string colorToUse = (this.comboColors.SelectedItem as StackPanel).Tag.ToString();
            this.MyInkCanvas.DefaultDrawingAttributes.Color =
                  (Color)ColorConverter.ConvertFromString(colorToUse);
        }

        private void RadioButtonClicked(object sender, RoutedEventArgs e)
        {
            // В зависимости от того, какая кнопка отправила событие,
            // поместить InkCanvas в нужный режим оперирования.

            switch ((sender as RadioButton)?.Content.ToString())
            {
                // Эти строки должны совпадать со значениями свойства Content
                // каждого элемента RadioButton.
                case "Ink Mode!":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "Erase Mode!":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
                case "Select Mode!":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
            }
        }

        private void SaveData(object sender, RoutedEventArgs e)
        {
            // Сохранить все данные InkCanvas в локальном файле.

            using (FileStream fs = new FileStream("StrokeData.bin", FileMode.Create))
            {
                this.MyInkCanvas.Strokes.Save(fs);
                fs.Close();
            }


        }
        private void LoadData(object sender, RoutedEventArgs e)
        {
            // Наполнить StrokeCollection из файла.
            using (FileStream fs = new FileStream("StrokeData.bin",
            FileMode.Open, FileAccess.Read))
            {
                StrokeCollection strokes = new StrokeCollection(fs);
                this.MyInkCanvas.Strokes = strokes;
            }
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            // Очистить все штрихи.
            this.MyInkCanvas.Strokes.Clear();
        }

    }
}
