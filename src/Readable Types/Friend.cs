using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kalakoi.Xbox.OpenXBL
{
    public class Friend
    {
        public string xuid { get; set; }
        public bool Favorite { get; set; }
        public bool IsFriend { get; set; }
        public bool IsFollower { get; set; }
        public bool SharedIdentity { get; set; }
        public DateTime DateAdded { get; set; }
        public string DisplayName { get; set; }
        public string RealName { get; set; }
        public Uri ProfilePicture { get; set; }
        public string Gamertag { get; set; }
        public int Gamerscore { get; set; }
        public string Reputation { get; set; }
        public string PresenceState { get; set; }
        public string PresenceText { get; set; }
        public MultiplayerSummary Summary { get; set; }
        public PreferredColor Color { get; set; }

        public Friend(string JSON) : this(Person.DeserializeJSON(JSON)) { }

        public Friend(Person person)
        {
            xuid = person.xuid;
            Favorite = person.isFavorite;
            IsFriend = person.isFollowedByCaller;
            IsFollower = person.isFollowingCaller;
            SharedIdentity = person.isIdentityShared;
            DateAdded = Convert.ToDateTime(person.addedDateTimeUtc);
            DisplayName = person.displayName;
            RealName = person.realName;
            ProfilePicture = new Uri(person.displayPicRaw);
            Gamertag = person.gamertag;
            Gamerscore = Convert.ToInt32(person.gamerScore);
            Reputation = person.xboxOneRep;
            PresenceState = person.presenceState;
            PresenceText = person.presenceText;
            Summary = person.multiplayerSummary;
            Color = person.preferredColor;
        }

        public static async Task<List<Friend>> GetFriendsAsync() => await XboxConnection.GetFriendsAsync().ConfigureAwait(false);

        public static List<Friend> GetFriends() => new List<Friend>(XboxConnection.GetFriends());
    }
}
