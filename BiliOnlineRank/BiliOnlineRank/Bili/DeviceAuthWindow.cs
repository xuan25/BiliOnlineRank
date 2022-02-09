using JsonUtil;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Bili
{
    internal class DeviceAuthWindow : Window
    {
        public string Code { get; private set; }

        private ManualResetEvent CodeObtainedEvent { get; set; }
        private WebView2 AuthView { get; set; }

        public DeviceAuthWindow(Uri source)
        {
            CodeObtainedEvent = new ManualResetEvent(false);

            AuthView = new WebView2();
            this.Content = AuthView;

            AuthView.Source = source;
            AuthView.CoreWebView2InitializationCompleted += AuthView_CoreWebView2InitializationCompleted;
        }

        public string GetCode()
        {
            CodeObtainedEvent.WaitOne();
            return Code;
        }

        private void AuthView_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            AuthView.CoreWebView2.WebResourceResponseReceived += CoreWebView2_WebResourceResponseReceived;
            AuthView.CoreWebView2.WebResourceRequested += CoreWebView2_WebResourceRequested;
            AuthView.CoreWebView2.AddWebResourceRequestedFilter(null, Microsoft.Web.WebView2.Core.CoreWebView2WebResourceContext.All);
        }

        private void CoreWebView2_WebResourceRequested(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebResourceRequestedEventArgs e)
        {
            //if (e.Request.Uri.Contains("/x/safecenter/sms/send"))
            //{
            //    Microsoft.Web.WebView2.Core.CoreWebView2Deferral coreWebView2Deferral = e.GetDeferral();
            //    Stream contentStream = e.Request.Content;
            //    StreamReader streamReader = new StreamReader(contentStream);
            //    string content = streamReader.ReadToEnd();

            //    Console.WriteLine(content);


            //    string captchaKey = Regex.Match(content, "&captcha_key=([0-9a-z]+)").Groups[1].Value;
            //    string challenge = Regex.Match(content, "&challenge=([0-9a-z]+)").Groups[1].Value;
            //    string validate = Regex.Match(content, "&validate=([0-9a-z]+)").Groups[1].Value;
            //    string tmpToken = Regex.Match(content, "&tmp_code=([0-9a-z]+)").Groups[1].Value;

            //    string smsReqContent = $"csrf=&csrf_token=&type=18&captcha_type=7&captcha_key={captchaKey}&captcha=&challenge={challenge}&seccode={validate}%7Cjordan&validate={validate}&tmp_code={tmpToken}";

            //    //Dictionary<string, string> payload = new Dictionary<string, string>()
            //    //{
            //    //    { "csrf", string.Empty },
            //    //    { "csrf_token", string.Empty },
            //    //    { "type", "18" },
            //    //    { "captcha_type", "7" },
            //    //    { "captcha_key", captchaKey },
            //    //    { "captcha", string.Empty },
            //    //    { "challenge", challenge },
            //    //    { "seccode", validate + "|jordan" },
            //    //    { "validate", validate },
            //    //    { "tmp_code", tmpToken }
            //    //};

            //    Dictionary<string, string> payload = new Dictionary<string, string>()
            //    {
            //        { "csrf", string.Empty },
            //        { "csrf_token", string.Empty },
            //        { "type", "17" },
            //        { "captcha_type", "5" },
            //        { "captcha_key", captchaKey },
            //        { "captcha", string.Empty },
            //        { "challenge", challenge },
            //        { "seccode", validate + "|jordan" },
            //        { "validate", validate },
            //        { "tmp_code", tmpToken }
            //    };

            //    string authUrl = "https://api.bilibili.com/x/safecenter/sms/send";

            //    Json.Value res = BiliApi.RequestJsonResult(authUrl, payload, false, "POST");

            //    MemoryStream memoryStream = new MemoryStream();
            //    StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.Default, 1024, true);
            //    //streamWriter.Write("{\"code\":0,\"message\":\"0\",\"ttl\":1,\"data\":{}}");
            //    streamWriter.Write(res.ToString());
            //    streamWriter.Close();
            //    memoryStream.Position = 0;
            //    AuthView.CoreWebView2.Environment.CreateWebResourceResponse(memoryStream, 200, null, null);



            //    //Dictionary<string, string> payload = new Dictionary<string, string>()
            //    //{
            //    //    { "channel", "bili" },
            //    //    { "code", smsCode },
            //    //    { "csrf", string.Empty },
            //    //    { "csrf_token", string.Empty },
            //    //    { "mobi_app", "android" },
            //    //    { "platform", "android" },
            //    //    { "statistics", $"{{\"appId\":1,\"platform\":3,\"version\":\"{BiliLogin.version}\",\"abtest\":\"\"}}" },
            //    //    { "tmp_token", tmpToken },
            //    //    { "ts", ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString() }
            //    //};

            //    //string authUrl = "https://passport.bilibili.com/api/login/verify_device";

            //    //Json.Value res = BiliApi.RequestJsonResult(authUrl, payload, true, "POST");

            //    //switch ((int)res["code"])
            //    //{
            //    //    case 0:
            //    //        string authCode = res["data"]["code"];
            //    //        return authCode;
            //    //    default:
            //    //        throw new Exception();
            //    //}
            //}

            //if (e.Request.Uri.Contains("/x/safecenter/tel/verify"))
            //{
            //    Stream contentStream = e.Request.Content;
            //    StreamReader streamReader = new StreamReader(contentStream);
            //    string content = streamReader.ReadToEnd();

            //    Console.WriteLine(content);

            //    string smsCode = Regex.Match(content, "&code=([0-9]+)").Groups[1].Value;
            //    string tmpToken = Regex.Match(content, "&tmp_code=([0-9a-z]+)").Groups[1].Value;

            //    string authCode = Verify(smsCode, tmpToken);

            //    Code = authCode;

            //    e.Response = AuthView.CoreWebView2.Environment.CreateWebResourceResponse(null, 200, null, null);
            //    CodeObtainedEvent.Set();
            //}
        }

        //public static string Verify(string smsCode, string tmpToken)
        //{
        //    Dictionary<string, string> payload = new Dictionary<string, string>()
        //    {
        //        { "channel", "bili" },
        //        { "code", smsCode },
        //        { "csrf", string.Empty },
        //        { "csrf_token", string.Empty },
        //        { "mobi_app", "android" },
        //        { "platform", "android" },
        //        { "statistics", $"{{\"appId\":1,\"platform\":3,\"version\":\"{BiliLogin.version}\",\"abtest\":\"\"}}" },
        //        { "tmp_token", tmpToken },
        //        { "ts", ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString() }
        //    };

        //    string authUrl = "https://passport.bilibili.com/api/login/verify_device";

        //    Json.Value res = BiliApi.RequestJsonResult(authUrl, payload, true, "POST");

        //    switch ((int)res["code"])
        //    {
        //        case 0:
        //            string authCode = res["data"]["code"];
        //            return authCode;
        //        default:
        //            throw new Exception();
        //    }
        //}

        private async void CoreWebView2_WebResourceResponseReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebResourceResponseReceivedEventArgs e)
        {
            if (e.Request.Uri.Contains("/x/safecenter/tel/verify"))
            {
                Console.WriteLine(e.Request.Uri);
                Stream stream = await e.Response.GetContentAsync();
                StreamReader streamReader = new StreamReader(stream);
                string content = await streamReader.ReadToEndAsync();
                Console.WriteLine(content);

                Json.Value json = Json.Parser.Parse(content);
                if (json["code"] == 0)
                {
                    Code = json["data"]["code"];
                }
                CodeObtainedEvent.Set();
            }

            //if (e.Request.Uri.Contains("/web/captcha/combine"))
            //{
            //    Stream stream = await e.Response.GetContentAsync();
            //    StreamReader streamReader = new StreamReader(stream);
            //    string content = await streamReader.ReadToEndAsync();
            //    Console.WriteLine(content);

            //    g_key = reg_key.IsMatch(content) ? reg_key.Match(content).Groups[1].Value : null;
            //}

            //if (e.Request.Uri.Contains("/ajax.php"))
            //{
            //    Stream stream = await e.Response.GetContentAsync();
            //    StreamReader streamReader = new StreamReader(stream);
            //    string content = await streamReader.ReadToEndAsync();
            //    Console.WriteLine(content);

            //    string validate = reg_validate.IsMatch(content) ? reg_validate.Match(content).Groups[1].Value : null;

            //    if (!string.IsNullOrEmpty(validate))
            //    {
            //        g_validate = validate;
            //    }
            //}
        }
    }
}
