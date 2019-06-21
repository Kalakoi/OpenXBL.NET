using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kalakoi.Xbox.OpenXBL
{
    public class PreferredColor
    {
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }

        public static PreferredColor DeserializeJSON(string JSON) => DeserializeJSON(JObject.Parse(JSON));

        public static PreferredColor DeserializeJSON(JToken token)
        {
            PreferredColor c = new PreferredColor()
            {
                PrimaryColor = (string)token.SelectToken("primaryColor"),
                SecondaryColor = (string)token.SelectToken("secondaryColor"),
                TertiaryColor = (string)token.SelectToken("tertiaryColor")
            };
            return c;
        }
    }
}
