using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kalakoi.Xbox.OpenXBL
{
    public class MultiplayerSummary
    {
        public bool InMultiplayerSession { get; set; }
        public bool InParty { get; set; }

        public static MultiplayerSummary DeserializeJSON(string JSON) => DeserializeJSON(JObject.Parse(JSON));

        public static MultiplayerSummary DeserializeJSON(JToken token)
        {
            MultiplayerSummary c = new MultiplayerSummary()
            {
                InMultiplayerSession = ((int)token.SelectToken("InMultiplayerSession") > 0),
                InParty = ((int)token.SelectToken("InParty") > 0)
            };
            return c;
        }
    }
}
