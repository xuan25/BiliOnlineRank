﻿using JsonUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace Bili.Models
{
    /// <summary>
    /// Class <c>LoginInfo</c> models information of bilibili login
    /// </summary>
    [Serializable]
    public class LoginInfo
    {
        /// <summary>
        /// Login token
        /// </summary>
        public LoginToken Token { get; set; }
        /// <summary>
        /// Login cookies
        /// </summary>
        public CookieCollection LoginCookies { get; set; }
        /// <summary>
        /// SSO info
        /// </summary>
        public List<string> SSO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json">json object</param>
        public LoginInfo(Json.Value json)
        {
            Token = new LoginToken(json["token_info"]);

            LoginCookies = new CookieCollection();
            foreach (Json.Value cookie in json["cookie_info"]["cookies"])
            {
                foreach (Json.Value domain in json["cookie_info"]["domains"])
                {
                    Cookie c = new Cookie(cookie["name"], cookie["value"], "/", domain)
                    {
                        Expires = new DateTime(1970, 1, 1).AddSeconds(cookie["expires"]),
                        HttpOnly = (int)cookie["http_only"] != 0
                    };
                    LoginCookies.Add(c);
                }
            }

            SSO = new List<string>();
            foreach (Json.Value sso in json["sso"])
            {
                SSO.Add(sso);
            }
        }

        public override string ToString()
        {
            
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("LoginToken:");
            stringBuilder.AppendLine("  " + Token.ToString().Replace("\n", "\n  "));

            stringBuilder.AppendLine("LoginCookies:");
            foreach (Cookie cookie in LoginCookies)
            {
                stringBuilder.AppendLine($"  {cookie.Name}={cookie.Value}; {cookie.Domain}{cookie.Path}; {cookie.Expires}");
            }

            stringBuilder.AppendLine("SSO:");
            foreach (string sso in SSO)
            {
                stringBuilder.AppendLine($"  {sso}");
            }
            return stringBuilder.ToString().Trim();
        }
    }

}
