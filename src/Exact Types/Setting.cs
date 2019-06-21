using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kalakoi.Xbox.OpenXBL
{
    public class Setting
    {
        public string id { get; set; }
        public string value { get; set; }

        public static Setting[] DeserializeJSON(JToken token)
        {
            //JToken token = JObject.Parse(JSON);
            List<Setting> settings = new List<Setting>();
            //Setting[] settings = new Setting[token.Children().Count()];
            foreach (JToken t in token.Children())
            {
                settings.Add(
                    new Setting()
                    {
                        id = (string)t.SelectToken("id"),
                        value = (string)t.SelectToken("value")
                    });
            }
            return settings.ToArray();
        }
    }
}
