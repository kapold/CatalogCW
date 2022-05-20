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
    /// Логика взаимодействия для AddNewParams.xaml
    /// </summary>
    public partial class AddNewParams : Window
    {
        public AddNewParams()
        {
            InitializeComponent();
        }

        private void AddGoodTypeBtn(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(goodTypeBox.Text))
            {
                MessageBox.Show("Введите название типа!");
                return;
            }

            try
            {
                DataBase db = new DataBase();
                db.AddGoodType(goodTypeBox.Text);
                db.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления типа!");
            }

            goodTypeBox.Text = String.Empty;
        }

        private void AddDisplayTypeBtn(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(displayTypeBox.Text))
            {
                MessageBox.Show("Введите название типа!");
                return;
            }

            try
            {
                DataBase db = new DataBase();
                db.AddDisplayType(displayTypeBox.Text);
                db.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления типа!");
            }

            displayTypeBox.Text = String.Empty;
        }

        private void AddResolutionBtn(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(resolutionTypeBox.Text))
            {
                MessageBox.Show("Введите название разрешения!");
                return;
            }

            try
            {
                DataBase db = new DataBase();
                db.AddResolution(resolutionTypeBox.Text);
                db.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления разрешения!");
            }

            resolutionTypeBox.Text = String.Empty;
        }

        private void AddHertzBtn(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(hertzTypeBox.Text))
            {
                MessageBox.Show("Введите название герц!");
                return;
            }

            try
            {
                DataBase db = new DataBase();
                db.AddHertz(hertzTypeBox.Text);
                db.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления герц!");
            }

            hertzTypeBox.Text = String.Empty;
        }

        private void AddRamBtn(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(ramTypeBox.Text))
            {
                MessageBox.Show("Введите название RAM!");
                return;
            }

            try
            {
                DataBase db = new DataBase();
                db.AddRAM(ramTypeBox.Text);
                db.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления RAM!");
            }

            ramTypeBox.Text = String.Empty;
        }

        private void AddRomBtn(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(romTypeBox.Text))
            {
                MessageBox.Show("Введите название ROM!");
                return;
            }

            try
            {
                DataBase db = new DataBase();
                db.AddROM(romTypeBox.Text);
                db.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления ROM!");
            }

            romTypeBox.Text = String.Empty;
        }

        private void AddOSBtn(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(osTypeBox.Text))
            {
                MessageBox.Show("Введите название OS!");
                return;
            }

            try
            {
                DataBase db = new DataBase();
                db.AddOS(osTypeBox.Text);
                db.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления OS!");
            }

            osTypeBox.Text = String.Empty;
        }

        private void AddColorBtn(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(colorTypeBox.Text))
            {
                MessageBox.Show("Введите название Color!");
                return;
            }

            try
            {
                DataBase db = new DataBase();
                db.AddColor(colorTypeBox.Text);
                db.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления Color!");
            }

            colorTypeBox.Text = String.Empty;
        }

        private void AddFirmBtn(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(firmTypeBox.Text))
            {
                MessageBox.Show("Введите название Firm!");
                return;
            }

            try
            {
                DataBase db = new DataBase();
                db.AddFirm(firmTypeBox.Text);
                db.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления Firm!");
            }

            firmTypeBox.Text = String.Empty;
        }
    }
}
