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
using AdriaanPetrusVanSchalkwyk.Model;

namespace AdriaanPetrusVanSchalkwyk.Pages
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Page
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void Welcome_Loaded(object sender, RoutedEventArgs e)
        {
           
           
        }

       
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Global.LoggedInUser = null;
            NavigationService.Navigate(new LoginPage());
        }
    }
}
