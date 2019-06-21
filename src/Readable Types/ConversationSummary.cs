using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kalakoi.Xbox.OpenXBL
{
    public class ConversationSummary
    {
        public long SenderXUID { get; set; }
        public string SenderGamertag { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime LastSent { get; set; }
        public int MessageCount { get; set; }
        public int UnreadMessageCount { get; set; }
        public Message LastMessage { get; set; }

        public static ConversationSummary DeserializeJSON(string JSON) => DeserializeJSON(JObject.Parse(JSON));

        public static ConversationSummary DeserializeJSON(JToken Token)
        {
            ConversationSummary c = new ConversationSummary()
            {
                SenderXUID = (long)Token.SelectToken("senderXuid"),
                SenderGamertag = (string)Token.SelectToken("senderGamerTag"),
                LastUpdated = (DateTime)Token.SelectToken("lastUpdated"),
                LastSent = (DateTime)Token.SelectToken("lastSent"),
                MessageCount = (int)Token.SelectToken("messageCount"),
                UnreadMessageCount = (int)Token.SelectToken("unreadMessageCount"),
                LastMessage = Message.DeserializeJSON(Token.SelectToken("lastMessage"))
            };
            return c;
        }
    }
}
