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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Catalog.Pages
{
    /// <summary>
    /// Логика взаимодействия для GoodDescriptionPage.xaml
    /// </summary>
    public partial class GoodDescriptionPage : Page
    {
        public GoodDescriptionPage(Good good)
        {
            InitializeComponent();

            imageDescription.Source = new BitmapImage(new Uri(good.ImageSrc, UriKind.Absolute));
            goodNameDescription.Text = good.Name;
            fullDecription.Text = good.FullDecsription();
        }
    }
}
