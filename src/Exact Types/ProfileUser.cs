using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kalakoi.Xbox.OpenXBL
{
    public class ProfileUser
    {
        public string id { get; set; }
        public string hostId { get; set; }
        public Setting[] settings { get; set; }
        public bool isSponsoredUser { get; set; }

        public static ProfileUser DeserializeJSON(string JSON)
        {
            JToken token = JObject.Parse(JSON);
            token = token.First.First.First;

            ProfileUser u = new ProfileUser()
            {
                id = (string)token.SelectToken("id"),
                hostId = (string)token.SelectToken("hostId"),
                isSponsoredUser = (bool)token.SelectToken("isSponsoredUser")
            };

            u.settings = Setting.DeserializeJSON(token.SelectToken("settings"));

            return u;
        }
    }
}
