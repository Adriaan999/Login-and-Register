using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdriaanPetrusVanSchalkwyk.Model;
using Newtonsoft.Json;
using System.Net;
namespace AdriaanPetrusVanSchalkwyk.Operations
{
    class Token
    {
        private string path;
        public Token()
        {
            this.path = "http://loacalhost/";
        }
        public User checkUserToken(string username, string password)
        {
            string endpoint = this.path + "/Users/getPerson";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<User>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public User GetUserDetails(User user)
        {
            string endpoint = this.path + "/users/" + user.Id;
            string access_token = user.access_token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = access_token;
            try
            {
                string response = wc.DownloadString(endpoint);
                user = JsonConvert.DeserializeObject<User>(response);
                user.access_token = access_token;
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public User RegisterUser(string username, string password)
        {
            string endpoint = this.path + "/users/addPerson";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password,
             
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
             try
             {
            string response = wc.UploadString(endpoint, method, json);
            return JsonConvert.DeserializeObject<User>(response);
             }
             catch (Exception)
              {
                  return null;
              }
        }
    }
}
