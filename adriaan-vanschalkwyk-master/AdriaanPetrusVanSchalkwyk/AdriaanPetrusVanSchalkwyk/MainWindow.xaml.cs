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
using System.IO;
using System.Net;
using AdriaanPetrusVanSchalkwyk.Pages;

namespace AdriaanPetrusVanSchalkwyk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!Directory.Exists("C:\\userData"))
                Directory.CreateDirectory("C:\\userData");
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://localhost/Users/test.txt");
            request.Credentials = new NetworkCredential("Dev", "dev");
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                MessageBox.Show("Connected to file server");
             
            }
            catch
            {
                MessageBox.Show("Not connected to file server");
            }
        }
    }
}
