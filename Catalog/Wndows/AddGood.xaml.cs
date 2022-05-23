using Catalog.Classes;
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
using System.Windows.Shapes;

namespace Catalog.Wndows
{
    /// <summary>
    /// Логика взаимодействия для AddGood.xaml
    /// </summary>
    public partial class AddGood : Window
    {
        Good good;
        public AddGood()
        {
            InitializeComponent();
            DataBase db = new DataBase();
            this.DataContext = new GoodViewModel();

            typeBox.ItemsSource = db.GetGoodTypes();
            displayTypeBox.ItemsSource = db.GetDisplayTypes();
            resolutionBox.ItemsSource = db.GetResolutions();
            hertzBox.ItemsSource = db.GetHertz();
            ramBox.ItemsSource = db.GetRAM();
            romBox.ItemsSource = db.GetROM();
            osBox.ItemsSource = db.GetOS();
            firmBox.ItemsSource = db.GetFirms();
            colorBox.ItemsSource = db.GetColors();

            // Error Provider


            db.Dispose();
        }

        private void AddNewGood(object sender, RoutedEventArgs e)
        {
            good = new Good();
            try
            {
                // Товар
                good.Name = nameBox.Text;
                good.Price = Convert.ToDouble(priceBox.Text);
                good.Count = Convert.ToInt32(countBox.Text);
                good.Firm = firmBox.Text;
                good.Description = descriptionBox.Text;
                good.ImageSrc = imageBox.Text;
                good.Type = (typeBox.SelectedItem as ComboBoxItem).Content.ToString();
                // Параметры
                good.Display = Convert.ToDouble(displaySizeBox.Text);
                good.DisplayType = (displayTypeBox.SelectedItem as ComboBoxItem).Content.ToString();
                good.Resolution = (resolutionBox.SelectedItem as ComboBoxItem).Content.ToString();
                good.Hertz = Convert.ToInt32((hertzBox.SelectedItem as ComboBoxItem).Content.ToString());
                good.CPU = cpuBox.Text;
                good.RAM = Convert.ToInt32((ramBox.SelectedItem as ComboBoxItem).Content.ToString());
                good.ROM = Convert.ToInt32((romBox.SelectedItem as ComboBoxItem).Content.ToString());
                good.Color = colorBox.Text;
                good.OS = (osBox.SelectedItem as ComboBoxItem).Content.ToString();
                good.Battery = Convert.ToInt32(batteryBox.Text);
                good.Camera = Convert.ToInt32(cameraBox.Text);
                if(nfcBox.Text == "Присутствует")
                    good.NFC = true;
                else 
                    good.NFC = false;

                //MessageBox.Show(good.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            DataBase dataBase = new DataBase();
            dataBase.AddGood(good);
            dataBase.Dispose();
            MessageBox.Show("Товар добавлен!");
        }

        private void ClearAllFields(object sender, RoutedEventArgs e)
        {
            nameBox.Text = null;
            priceBox.Text = null;
            countBox.Text = null;
            firmBox.Text = null;
            typeBox.Text = null;
            imageBox.Text = null;
            descriptionBox.Text = null;
            displaySizeBox.Text = null;
            displayTypeBox.Text = null;
            resolutionBox.Text = null;
            hertzBox.Text = null;
            cpuBox.Text = null;
            colorBox.Text = null;
            ramBox.Text = null;
            romBox.Text = null;
            osBox.Text = null;
            batteryBox.Text = null;
            cameraBox.Text = null;
            nfcBox.Text = null;
        }
    }
}
