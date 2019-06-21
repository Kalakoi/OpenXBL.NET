using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kalakoi.Xbox.OpenXBL
{
    public class Conversation
    {
        public ConversationSummary Summary { get; set; }
        public List<Message> Messages { get; set; }

        public static Conversation DeserializeJSON(string JSON) => DeserializeJSON(JObject.Parse(JSON));

        public static Conversation DeserializeJSON(JToken Token)
        {
            Conversation c = new Conversation()
            {
                Summary = ConversationSummary.DeserializeJSON(Token.SelectToken("conversation").SelectToken("summary")),
                Messages = new List<Message>()
            };
            foreach (JToken t in Token.First.First.SelectToken("messages").Children())
                c.Messages.Add(Message.DeserializeJSON(t));
            return c;
        }
    }
}
