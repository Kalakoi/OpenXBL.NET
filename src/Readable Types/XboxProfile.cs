using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalakoi.Xbox.OpenXBL
{
    public class XboxProfile
    {
        public long ID { get; set; }
        public long HostID { get; set; }
        public Uri GamerPic { get; set; }
        public int Gamerscore { get; set; }
        public string Gamertag { get; set; }
        public string AccountTier { get; set; }
        public string Reputation { get; set; }
        public PreferredColor PreferredColor { get; set; }
        public string RealName { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        //public bool SponsoredUser { get; set; }

        public XboxProfile(string JSON) : this(ProfileUser.DeserializeJSON(JSON)) { }
        public XboxProfile(ProfileUser User)
        {
            ID = Convert.ToInt64(User.id);
            HostID = Convert.ToInt64(User.hostId);
            //SponsoredUser = User.isSponsoredUser;
            foreach (Setting s in User.settings)
            {
                switch (s.id)
                {
                    case "GameDisplayPicRaw":
                        GamerPic = new Uri(s.value);
                        break;
                    case "Gamerscore":
                        Gamerscore = Convert.ToInt32(s.value);
                        break;
                    case "Gamertag":
                        Gamertag = s.value;
                        break;
                    case "AccountTier":
                        AccountTier = s.value;
                        break;
                    case "XboxOneRep":
                        Reputation = s.value;
                        break;
                    case "PreferredColor":
                        string ColorJSON = RestServices.GetResponse(new Uri(s.value));
                        PreferredColor = PreferredColor.DeserializeJSON(ColorJSON);
                        break;
                    case "RealName":
                        RealName = s.value;
                        break;
                    case "Bio":
                        Bio = s.value;
                        break;
                    case "Location":
                        Location = s.value;
                        break;
                    default:
                        break;
                }
            }
        }
        
        public static async Task<XboxProfile> GetProfileAsync() => await XboxConnection.GetProfileAsync().ConfigureAwait(false);
        public static async Task<XboxProfile> GetProfileAsync(string Gamertag) => await XboxConnection.GetProfileAsync(Gamertag).ConfigureAwait(false);
        public static XboxProfile GetProfile() => XboxConnection.GetProfile();
        public static XboxProfile GetProfile(string Gamertag) => XboxConnection.GetProfile(Gamertag);
    }
}
