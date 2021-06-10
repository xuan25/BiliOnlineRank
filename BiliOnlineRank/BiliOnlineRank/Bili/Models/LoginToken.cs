using JsonUtil;
using System;

namespace Bili.Models
{
    [Serializable]
    public class LoginToken
    {
        public uint Mid { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }

        public LoginToken(Json.Value json)
        {
            Mid = json["mid"];
            AccessToken = json["access_token"];
            RefreshToken = json["refresh_token"];
            Expires = DateTime.Now.AddSeconds(json["expires_in"]);
        }

        public override string ToString()
        {
            return $"Mid: {Mid}\nAccessToken: {AccessToken}\nRefreshToken: {RefreshToken}\nExpires: {Expires}";
        }
    }

}
