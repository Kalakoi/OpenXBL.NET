using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kalakoi.Xbox.OpenXBL
{
    public class Alert
    {
        public string ID { get; set; }
        public string Action { get; set; }
        public string Path { get; set; }
        public string ActorXuid { get; set; }
        public string ActorGamertag { get; set; }
        public string ParentType { get; set; }
        public string ParentPath { get; set; }
        public string OwnerXuid { get; set; }
        public string OwnerGamertag { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Seen { get; set; }
        public string RootPath { get; set; }

        public static Alert DeserializeJSON(string JSON) => DeserializeJSON(JObject.Parse(JSON));

        public static Alert DeserializeJSON(JToken Token)
        {
            if (Token == null || !Token.HasValues) return new Alert();
            Alert a = new Alert()
            {
                ID = (string)Token.SelectToken("id"),
                Action = (string)Token.SelectToken("action"),
                Path = (string)Token.SelectToken("path"),
                ActorXuid = (string)Token.SelectToken("actorXuid"),
                ActorGamertag = (string)Token.SelectToken("actorGamertag"),
                ParentType = (string)Token.SelectToken("parentType"),
                ParentPath = (string)Token.SelectToken("parentPath"),
                OwnerXuid = (string)Token.SelectToken("ownerXuid"),
                OwnerGamertag = (string)Token.SelectToken("ownerGamertag"),
                Timestamp = (DateTime)Token.SelectToken("timestamp"),
                Seen = (bool)Token.SelectToken("seen"),
                RootPath = (string)Token.SelectToken("rootPath")
            };
            return a;
        }
    }
}
