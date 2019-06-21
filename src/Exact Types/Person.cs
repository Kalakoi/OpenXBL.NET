using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kalakoi.Xbox.OpenXBL
{
    public class Person
    {
        public string xuid { get; set; }
        public bool isFavorite { get; set; }
        public bool isFollowingCaller { get; set; }
        public bool isFollowedByCaller { get; set; }
        public bool isIdentityShared { get; set; }
        public string addedDateTimeUtc { get; set; }
        public string displayName { get; set; }
        public string realName { get; set; }
        public string displayPicRaw { get; set; }
        public bool useAvatar { get; set; }
        public string gamertag { get; set; }
        public string gamerScore { get; set; }
        public string xboxOneRep { get; set; }
        public string presenceState { get; set; }
        public string presenceText { get; set; }
        public string presenceDevices { get; set; }
        public bool isBroadcasting { get; set; }
        public bool? isCloaked { get; set; }
        public bool isQuarantined { get; set; }
        public object suggestion { get; set; }
        public object recommendation { get; set; }
        public object titleHistory { get; set; }
        public MultiplayerSummary multiplayerSummary { get; set; }
        public object recentPlayer { get; set; }
        public object follower { get; set; }
        public PreferredColor preferredColor { get; set; }
        public object presenceDetails { get; set; }
        public object titlePresence { get; set; }
        public object titleSummaries { get; set; }
        public object presenceTitleIds { get; set; }
        public object detail { get; set; }
        public object communityManagerTitles { get; set; }
        public SocialManager socialManager { get; set; }
        public object[] broadcast { get; set; }
        public object tournamentSummary { get; set; }
        public object avatar { get; set; }

        public static Person DeserializeJSON(string JSON) => DeserializeJSON(JObject.Parse(JSON));

        public static Person DeserializeJSON(JToken token)
        {
            Person p = new Person()
            {
                xuid = (string)token.SelectToken("xuid"),
                isFavorite = (bool)token.SelectToken("isFavorite"),
                isFollowingCaller = (bool)token.SelectToken("isFollowingCaller"),
                isFollowedByCaller = (bool)token.SelectToken("isFollowedByCaller"),
                isIdentityShared = (bool)token.SelectToken("isIdentityShared"),
                addedDateTimeUtc = (string)token.SelectToken("addedDateTimeUtc"),
                displayName = (string)token.SelectToken("displayName"),
                realName = (string)token.SelectToken("realName"),
                displayPicRaw = (string)token.SelectToken("displayPicRaw"),
                gamertag = (string)token.SelectToken("gamertag"),
                gamerScore = (string)token.SelectToken("gamerScore"),
                xboxOneRep = (string)token.SelectToken("xboxOneRep"),
                presenceState = (string)token.SelectToken("presenceState"),
                presenceText = (string)token.SelectToken("presenceText"),
                multiplayerSummary = MultiplayerSummary.DeserializeJSON(token.SelectToken("multiplayerSummary")),
                preferredColor = PreferredColor.DeserializeJSON(token.SelectToken("preferredColor"))
            };
            return p;
        }
    }
}
