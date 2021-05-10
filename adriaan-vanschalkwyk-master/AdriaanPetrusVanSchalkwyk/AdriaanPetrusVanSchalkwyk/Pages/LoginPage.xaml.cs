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
using System.Net;
using System.IO;
using Newtonsoft.Json;
using AdriaanPetrusVanSchalkwyk.Model;
using AdriaanPetrusVanSchalkwyk.Operations;
namespace AdriaanPetrusVanSchalkwyk.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
           
        }
        #region token apprach
        private void tokenApprach(string username,string password)
        {
            Token tkn = new Token();
            User user = tkn.checkUserToken(username, password);
            if (user == null)
            {
                MessageBox.Show("Invalid username or password");
                return;
            }

            Global.LoggedInUser = user;
            MessageBox.Show("Login successful");
            NavigationService.Navigate(new Welcome());
        }
        
        #endregion
        #region file approach
        private void FileApproach(string username, string password)
        {
            try
            {
                string path1 = "ftp://localhost/Users/" + username + ".txt";


                WebClient req = new WebClient();
                req.Credentials = new NetworkCredential("Dev", "dev");


                byte[] info = req.DownloadData(path1);
                string pass = System.Text.Encoding.UTF8.GetString(info);

                if (pass == password)
                {
                    NavigationService.Navigate(new Welcome());

                }
                else
                {
                    MessageBox.Show("Wrong");
                }
            }
            catch
            {
                MessageBox.Show("Please enter data");
            }

        }
        #endregion
        #region db approach
        private void getPerson(User p)
        {
            string json = JsonConvert.SerializeObject(p);

            string sendPersonURL = "http://localhost/Users/getUser.php";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sendPersonURL);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = json.Length;

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)request.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        if (result == "success")
                          {
                            NavigationService.Navigate(new Welcome());
                        }
                        else
                        {
                            MessageBox.Show("Username or password incorrect");
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                MessageBox.Show("Error connecting to webservice: " + wex.Message);
            }
        }
        private void dbApproach(string username, string password)
        {
           User tmp = new User(username, password);

            getPerson(tmp);
        }
        #endregion
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbxUsername.Text;
            string password = pbxPassword.Password;
            FileApproach(username, password); //comment this out to test db
            //dbApproach(username, password); remove comment to test db
            
        }
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
         
            NavigationService.Navigate(new Register());
        }

    }
  
}
