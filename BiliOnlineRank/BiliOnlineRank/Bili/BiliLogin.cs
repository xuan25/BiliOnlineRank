using Bili.Exceptions;
using Bili.Models;
using JsonUtil;
using RSAUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Bili
{
    /// <summary>
    /// Class <c>BiliLogin</c> wraps login methods for Bilibili.
    /// Author: Xuan525
    /// Date: 10 Jun 2021
    /// </summary>
    public static class BiliLogin
    {
        /// <summary>
        /// Login with username and password
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <param name="captcha">optional captcha</param>
        /// <returns>LoginInfo</returns>
        public static LoginInfo Login(string username, string password, string captcha = null)
        {
            string encryptedPassword = EncryptPassword(password);

            Dictionary<string, string> payload = new Dictionary<string, string>()
            {
                { "username", username },
                { "password", encryptedPassword },
            };
            if (captcha != null)
            {
                payload.Add("captcha", captcha);
            }

            string authUrl = "https://passport.bilibili.com/api/v3/oauth2/login";
            Json.Value res = BiliApi.RequestJsonResult(authUrl, payload, true, "POST");

            switch ((int)res["code"])
            {
                case 0:
                    switch ((int)res["data"]["status"])
                    {
                        case 0:
                            LoginInfo loginInfo = new LoginInfo(res["data"]);
                            return loginInfo;
                        default:
                            throw new LoginStatusException(res["data"]["status"], res);
                    }
                default:
                    throw new LoginFailedException(res["code"], res["message"]);
            }
        }

        /// <summary>
        /// Login with username and password asynchronously
        /// </summary>
        /// <param name="password">Plain password</param>
        /// <returns>Encrypted password</returns>
        public static Task<LoginInfo> LoginAsync(string username, string password, string captcha = null)
        {
            Task<LoginInfo> task = new Task<LoginInfo>(() =>
            {
                return Login(username, password, captcha);
            });
            task.Start();
            return task;
        }

        /// <summary>
        /// Encrypt password
        /// </summary>
        /// <param name="password">Plain password</param>
        /// <returns>Encrypted password</returns>
        private static string EncryptPassword(string password)
        {
            string pubkeyUrl = "https://passport.bilibili.com/api/oauth2/getKey";
            string resContent = BiliApi.RequestTextResult(pubkeyUrl, null, true, "POST");
            Json.Value val = Json.Parser.Parse(resContent);

            if (val["code"] == 0)
            {
                string key = val["data"]["key"];
                string hash = val["data"]["hash"];

                // RSA
                string encryptedPassword = RSAEncrypter.Encrypt($"{hash}{password}", key);
                return encryptedPassword;
            }
            else
            {
                throw new Exception();
            }

        }

        /// <summary>
        /// Refresh access token
        /// </summary>
        /// <param name="token">Old LoginToken</param>
        /// <returns>New LoginToken</returns>
        public static LoginToken RefreshToken(LoginToken token)
        {
            Dictionary<string, string> payload = new Dictionary<string, string>()
            {
                { "access_token", token.AccessToken },
                { "refresh_token", token.RefreshToken },
            };

            string refreshTokenUrl = "https://passport.bilibili.com/api/oauth2/refreshToken";
            Json.Value res = BiliApi.RequestJsonResult(refreshTokenUrl, payload, true, "POST");

            switch((int)res["code"])
            {
                case 0:
                    LoginToken loginToken = new LoginToken(res["data"]);
                    return loginToken;
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// Get captcha image
        /// </summary>
        /// <returns>A captcha image. Corresponding cookies will be set into BiliApi automatically</returns>
        public static BitmapImage GetCaptcha()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://passport.bilibili.com/captcha");
            request.CookieContainer = BiliApi.Cookies;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        stream.CopyTo(memoryStream);
                        memoryStream.Position = 0;
                    }
                }

                BitmapImage bitmapImage = new BitmapImage();

                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

    }
}
