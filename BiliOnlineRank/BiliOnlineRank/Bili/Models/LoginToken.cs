using JsonUtil;
using System;

namespace Bili.Models
{
    /// <summary>
    /// Class <c>LoginToken</c> models a bilibili login token 
    /// </summary>
    [Serializable]
    public class LoginToken
    {
        /// <summary>
        /// Member id of the user
        /// </summary>
        public uint Mid { get; set; }
        /// <summary>
        /// Access token 
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Token for refreshing the access token 
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// Expires date
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json">json object</param>
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
