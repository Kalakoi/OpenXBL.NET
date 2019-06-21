using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kalakoi.Xbox.OpenXBL
{
    public class Message
    {
        public int MessageID { get; set; }
        public int SenderTitleID { get; set; }
        public long SenderXUID { get; set; }
        public string SenderGamertag { get; set; }
        public DateTime SentTime { get; set; }
        public string MessageType { get; set; }
        public string MessageFolder { get; set; }
        public bool Read { get; set; }
        public bool HasPhoto { get; set; }
        public bool HasAudio { get; set; }
        public string Text { get; set; }

        public static Message DeserializeJSON(string JSON) => DeserializeJSON(JObject.Parse(JSON));
        public static Message DeserializeJSON(JToken Token)
        {
            if (Token == null || !Token.HasValues) return new Message();
            Message m = new Message()
            {
                MessageID = (int)Token.SelectToken("messageId"),
                SenderTitleID = (int)Token.SelectToken("senderTitleId"),
                SenderXUID = (long)Token.SelectToken("senderXuid"),
                SenderGamertag = (string)Token.SelectToken("senderGamerTag"),
                SentTime = (DateTime)Token.SelectToken("sentTime"),
                MessageType = (string)Token.SelectToken("messageType"),
                MessageFolder = (string)Token.SelectToken("messageFolder"),
                Read = (bool)Token.SelectToken("isRead"),
                HasPhoto = (bool)Token.SelectToken("hasPhoto"),
                HasAudio = (bool)Token.SelectToken("hasAudio"),
                Text = (string)Token.SelectToken("messageText")
            };
            return m;
        }
    }
}
