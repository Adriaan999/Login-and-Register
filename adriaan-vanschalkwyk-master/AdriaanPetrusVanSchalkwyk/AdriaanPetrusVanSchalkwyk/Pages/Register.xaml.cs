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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }
        #region check for file
        private bool check(string name)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(name);
            request.Credentials = new NetworkCredential("Dev", "dev");
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return false;
        }
        #endregion
        #region file register apprach
        private void fileRegApproach(string username, string password)
        {
            bool checkFile;
            string path = "ftp://localhost/Users/" + username + ".txt";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);

            request.Credentials = new NetworkCredential("Dev", "dev");
            checkFile = check(path);


            if (checkFile == false)
            {
               
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    using (StreamWriter swr = File.CreateText(@"c:\userData\" + username + ".txt"))
                    {
                        swr.Write(password);
                    }

                    byte[] content;
                    using (StreamReader sr = new StreamReader(@"c:\userData\" + username + ".txt"))
                    {
                        content = Encoding.UTF8.GetBytes(sr.ReadToEnd());
                    }

                    using (Stream sw = request.GetRequestStream())
                    {
                        sw.Write(content, 0, content.Length);
                    }


                    File.Delete(@"c:\userData\" + username + ".txt");

                    NavigationService.Navigate(new Welcome());

            }
            else
            {
                MessageBox.Show("Username already used");
            }

        }
        #endregion
        #region db apprach
        private void dbRegperson(string username, string password)
        {
            User tmp = new User(username, password);

            addPerson(tmp);
        }
        private void addPerson(User p)
        {
            string json = JsonConvert.SerializeObject(p);

            string sendPersonURL = "http://localhost/Users/addUser.php";
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
        #endregion
        #region tokenApprach
        private void RegToken(string username, string password)
        {
            Token tkn = new Token();
        User user = tkn.RegisterUser(username, password);
            if (user == null)
            {
                MessageBox.Show("Username already exists");
                return;
            }
            Global.LoggedInUser = user;
            MessageBox.Show("Registration successful");
            NavigationService.Navigate(new Welcome());
        }
       

         
        #endregion
        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            
            string username = tbxUsername.Text;
            string password = pbxPassword.Password;
            string cpassword = pbxcPassword.Password;

   
                    if (password == cpassword)
                    {
                        fileRegApproach(username, password); //comment this out to test db
                       // dbRegperson(username, password); remove comment to test db
                    }
                    else
                    {
                         MessageBox.Show("Passwords do not match");
                    }

      }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
