using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaanPetrusVanSchalkwyk.Model
{
    class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string access_token { get; set; }
        public int Id { get; set; }
        public User(string username, string password)
        {

            this.username = username;
            this.password = password;

        }
    }
}
