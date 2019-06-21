using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Kalakoi.Xbox.OpenXBL
{
    public static class XboxConnection
    {
        public const string BASEURI = "https://xbl.io/api/v2";
        
        public static string APIKEY = string.Empty;

        public static void SetApiKey(string Key)
        {
            APIKEY = Key;
        }

        public static async Task<string> XuidFromGamertagAsync(string Gamertag)
        {
            string Endpoint = string.Format("/friends/search?gt={0}", Gamertag);
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetResponseAsync(RequestAddress, APIKEY).ConfigureAwait(false);
            XboxProfile p = new XboxProfile(Response);
            return p.ID.ToString();
        }

        public static string XuidFromGamertag(string Gamertag)
        {
            string Endpoint = string.Format("/friends/search?gt={0}", Gamertag);
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            XboxProfile p = new XboxProfile(Response);
            return p.ID.ToString();
        }

        public static async Task<XboxProfile> GetProfileAsync()
        {
            string Endpoint = "/account";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetResponseAsync(RequestAddress, APIKEY).ConfigureAwait(false);
            return new XboxProfile(Response);
        }

        public static XboxProfile GetProfile()
        {
            string Endpoint = "/account";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            return new XboxProfile(Response);
        }

        public static async Task<XboxProfile> GetProfileAsync(string Gamertag)
        {
            string Endpoint = string.Format("/friends/search?gt={0}", Gamertag);
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetResponseAsync(RequestAddress, APIKEY).ConfigureAwait(false);
            return new XboxProfile(Response);
        }

        public static XboxProfile GetProfile(string Gamertag)
        {
            string Endpoint = string.Format("/friends/search?gt={0}", Gamertag);
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            return new XboxProfile(Response);
        }

        public static async Task<List<Friend>> GetFriendsAsync()
        {
            string Endpoint = "/friends";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetResponseAsync(RequestAddress, APIKEY).ConfigureAwait(false);
            List<Friend> Return = new List<Friend>();
            List<JToken> tokens = new List<JToken>(JObject.Parse(Response).SelectTokens("people"));
            foreach (JToken t in tokens.Children())
                Return.Add(new Friend(Person.DeserializeJSON(t)));
            return Return;
        }

        public static IEnumerable<Friend> GetFriends()
        {
            string Endpoint = "/friends";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            List<JToken> tokens = new List<JToken>(JObject.Parse(Response).SelectTokens("people"));
            foreach (JToken t in tokens.Children())
                yield return new Friend(Person.DeserializeJSON(t));
        }

        public static async Task<List<Friend>> GetFriendsAsync(string xuid)
        {
            string Endpoint = "/friends?xuid=";
            Uri RequestAddress = new Uri(string.Format("{0}{1}{2}", BASEURI, Endpoint, xuid));
            string Response = await RestServices.GetResponseAsync(RequestAddress, APIKEY).ConfigureAwait(false);
            List<Friend> Return = new List<Friend>();
            List<JToken> tokens = new List<JToken>(JObject.Parse(Response).SelectTokens("people"));
            foreach (JToken t in tokens.Children())
                Return.Add(new Friend(Person.DeserializeJSON(t)));
            return Return;
        }

        public static IEnumerable<Friend> GetFriends(string xuid)
        {
            string Endpoint = "/friends?xuid=";
            Uri RequestAddress = new Uri(string.Format("{0}{1}{2}", BASEURI, Endpoint, xuid));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            List<JToken> tokens = new List<JToken>(JObject.Parse(Response).SelectTokens("people"));
            foreach (JToken t in tokens.Children())
                yield return new Friend(Person.DeserializeJSON(t));
        }

        public static async Task AddFriendAsync(string xuid)
        {
            string Endpoint = string.Format("/friends/add/{0}", xuid);
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetResponseAsync(RequestAddress, APIKEY).ConfigureAwait(false);
        }

        public static void AddFriend(string xuid)
        {
            string Endpoint = string.Format("/friends/add/{0}", xuid);
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
        }

        public static async Task RemoveFriendAsync(string xuid)
        {
            string Endpoint = string.Format("/friends/remove/{0}", xuid);
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetResponseAsync(RequestAddress, APIKEY).ConfigureAwait(false);
        }

        public static void RemoveFriend(string xuid)
        {
            string Endpoint = string.Format("/friends/remove/{0}", xuid);
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
        }

        public static async Task FavoriteFriendAsync(string xuid)
        {
            string Endpoint = "/friends/favorite";
            //string Data = string.Format("{\"xuids\":[{0}]}", xuid);
            string Data = "{\"xuids\":[" + xuid + "]}";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetPostResponseAsync(RequestAddress, Data, APIKEY).ConfigureAwait(false);
        }

        public static void FavoriteFriend(string xuid)
        {
            string Endpoint = "/friends/favorite";
            //string Data = string.Format("{\"xuids\":[{0}]}", xuid);
            string Data = "{\"xuids\":[" + xuid + "]}";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetPostResponse(RequestAddress, Data, APIKEY);
        }

        public static async Task UnfavoriteFriendAsync(string xuid)
        {
            string Endpoint = "/friends/favorite/remove";
            //string Data = string.Format("{\"xuids\":[{0}]}", xuid);
            string Data = "{\"xuids\":[" + xuid + "]}";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetPostResponseAsync(RequestAddress, Data, APIKEY).ConfigureAwait(false);
        }

        public static void UnfavoriteFriend(string xuid)
        {
            string Endpoint = "/friends/favorite/remove";
            //string Data = string.Format("{\"xuids\":[{0}]}", xuid);
            string Data = "{\"xuids\":[" + xuid +"]}";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetPostResponse(RequestAddress, Data, APIKEY);
        }

        public static async Task<List<Friend>> GetRecentPlayersAsync()
        {
            string Endpoint = "/recent-players";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetResponseAsync(RequestAddress, APIKEY).ConfigureAwait(false);
            List<Friend> Return = new List<Friend>();
            List<JToken> tokens = new List<JToken>(JObject.Parse(Response).SelectTokens("people"));
            foreach (JToken t in tokens.Children())
                Return.Add(new Friend(Person.DeserializeJSON(t)));
            return Return;
        }

        public static IEnumerable<Friend> GetRecentPlayers()
        {
            string Endpoint = "/recent-players";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            List<JToken> tokens = new List<JToken>(JObject.Parse(Response).SelectTokens("people"));
            foreach (JToken t in tokens.Children())
                yield return new Friend(Person.DeserializeJSON(t));
        }

        public static async Task<List<ConversationSummary>> GetConversationsAsync()
        {
            string Endpoint = "/conversations";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetResponseAsync(RequestAddress, APIKEY).ConfigureAwait(false);
            List<JToken> tokens = new List<JToken>(JObject.Parse(Response).SelectTokens("results"));
            List<ConversationSummary> l = new List<ConversationSummary>();
            foreach (JToken t in tokens.Children())
                l.Add(ConversationSummary.DeserializeJSON(t));
            return l;
        }

        public static IEnumerable<ConversationSummary> GetConversations()
        {
            string Endpoint = "/conversations";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            List<JToken> tokens = new List<JToken>(JObject.Parse(Response).SelectTokens("results"));
            foreach (JToken t in tokens.Children())
                yield return ConversationSummary.DeserializeJSON(t);
        }

        public static async Task<Conversation> GetConversationAsync(string xuid)
        {
            string Endpoint = string.Format("/conversations/{0}", xuid);
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetResponseAsync(RequestAddress, APIKEY).ConfigureAwait(false);
            return Conversation.DeserializeJSON(Response);
        }

        public static Conversation GetConversation(string xuid)
        {
            string Endpoint = string.Format("/conversations/{0}", xuid);
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            return Conversation.DeserializeJSON(Response);
        }

        public static async Task SendMessageAsync(string Gamertag, string Message)
        {
            string Endpoint = "/conversations";
            //string Data = string.Format("{\"to\":\"{0}\",\"message\":\"{1}\"}", Gamertag, Message);
            string Data = "{\"to\":\"" + Gamertag + "\",\"message\":\"" + Message + "\"}";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetPostResponseAsync(RequestAddress, Data, APIKEY).ConfigureAwait(false);
        }

        public static void SendMessage(string Gamertag, string Message)
        {
            string Endpoint = "/conversations";
            //string Data = string.Format("{\"to\":\"{0}\",\"message\":\"{1}\"}", Gamertag, Message);
            string Data = "{\"to\":\"" + Gamertag + "\",\"message\":\"" + Message + "\"}";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetPostResponse(RequestAddress, Data, APIKEY);
        }

        public static async Task<List<Alert>> GetAlertsAsync()
        {
            string Endpoint = "/alerts";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetResponseAsync(RequestAddress, APIKEY).ConfigureAwait(false);
            List<JToken> tokens = new List<JToken>(JObject.Parse(Response).SelectTokens("alerts"));
            List<Alert> l = new List<Alert>();
            foreach (JToken t in tokens.Children())
                l.Add(Alert.DeserializeJSON(t));
            return l;
        }

        public static IEnumerable<Alert> GetAlerts()
        {
            string Endpoint = "/alerts";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            List<JToken> tokens = new List<JToken>(JObject.Parse(Response).SelectTokens("alerts"));
            List<Alert> l = new List<Alert>();
            foreach (JToken t in tokens.Children())
                yield return Alert.DeserializeJSON(t);
        }

        public static string GetFeed()
        {
            string Endpoint = "/activity/feed";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            return Response;
        }

        public static string GetHistory()
        {
            string Endpoint = "/activity/history";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            return Response;
        }

        public static async Task<Friend> GetSummaryAsync()
        {
            string Endpoint = "/player/summary";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = await RestServices.GetResponseAsync(RequestAddress, APIKEY).ConfigureAwait(false);
            return new Friend(Response);
        }

        public static Friend GetSummary()
        {
            string Endpoint = "/player/summary";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            return new Friend(Response);
        }

        public static string GetClips()
        {
            string Endpoint = "/dvr/gameclips";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            return Response;
        }

        public static string GetScreenshots()
        {
            string Endpoint = "/dvr/screenshots";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            return Response;
        }

        public static string GetAchievements()
        {
            string Endpoint = "/achievements";
            Uri RequestAddress = new Uri(string.Format("{0}{1}", BASEURI, Endpoint));
            string Response = RestServices.GetResponse(RequestAddress, APIKEY);
            return Response;
        }

        //TODO: 
        //GET /account/{xuid}
        //GET /friends?xuid={xuid}
        //GET /presence
        //GET /{xuid}/presence
        //POST /generate/gamertag Payload: {"Algorithm":1,"Count":3,"Locale":"en-US","Seed":""} 
        //POST /clubs/recommendations
        //GET /clubs/owned
        //POST /clubs/create Payload: {"name":"Hello World", "type":"[public/private/hidden]"} 
        //GET /clubs/find?q={query}
        //POST /clubs/reserve Payload: {"name":"Hello World"} 
        //GET /activity/feed
        //POST /activity/feed
        //GET /activity/history
        //GET /dvr/gameclips
        //GET /dvr/screenshots
        //GET /achievements
        //GET /achievements/{titleid}
    }
}
