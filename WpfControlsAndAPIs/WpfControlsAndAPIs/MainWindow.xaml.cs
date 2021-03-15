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

        private void ColorChanged (object sender, SelectionChangedEventArgs e)
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

    }
}
