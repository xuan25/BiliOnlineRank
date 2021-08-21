using Bili.Models;
using JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BiliOnlineRank
{
    /// <summary>
    /// Class <c>ApiProvider</c> models a local Api service provider
    /// </summary>
    public class ApiProvider
    {
        HttpListener httpListener;
        Thread loopThread;
        public AnchorOnlineGoldRank GoldRank { get; set; }
        public OnlineRank Rank { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix">prefix (E.g. "http://localhost:8000/")</param>
        public ApiProvider(string prefix)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add(prefix);
        }

        /// <summary>
        /// Start the service
        /// </summary>
        public void Start()
        {
            httpListener.Start();

            loopThread = new Thread(ServerLoop);
            loopThread.Start();
        }

        /// <summary>
        /// Stop the service
        /// </summary>
        public void Stop()
        {
            loopThread.Abort();
            httpListener.Stop();
        }

        /// <summary>
        /// Request handling loop
        /// </summary>
        private void ServerLoop()
        {
            while (httpListener.IsListening)
            {
                HttpListenerContext ctx = httpListener.GetContext();

                // Process requests in other threads
                Task task = new Task(() => HandleContext(ctx));
                task.Start();
            }
        }

        /// <summary>
        /// Http request handler
        /// </summary>
        /// <param name="ctx">http context</param>
        private void HandleContext(HttpListenerContext ctx)
        {
            HttpListenerRequest req = ctx.Request;
            HttpListenerResponse res = ctx.Response;

            switch (req.HttpMethod)
            {
                case "GET":
                    switch (req.Url.AbsolutePath)
                    {
                        case "/":
                            IndexController(req, res);
                            break;
                        case "/data":
                            DataController(req, res);
                            break;
                        default:
                            res.StatusCode = 404;
                            res.Close();
                            break;
                    }
                    break;
                default:
                    res.StatusCode = 500;
                    res.Close();
                    break;
            }
        }

        /// <summary>
        /// Request handler for "/"
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        private void IndexController(HttpListenerRequest req, HttpListenerResponse res)
        {
            byte[] buffer = Encoding.UTF8.GetBytes("Listening...");
            res.ContentType = "text/plain";
            res.ContentEncoding = Encoding.UTF8;
            res.ContentLength64 = buffer.LongLength;

            res.OutputStream.Write(buffer, 0, buffer.Length);
            res.Close();
        }

        /// <summary>
        /// Request handler for "/data"
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        private void DataController(HttpListenerRequest req, HttpListenerResponse res)
        {
            Json.Value.Array goldRank = new Json.Value.Array();
            if (GoldRank != null)
            {
                foreach (AnchorOnlineGoldRankItem item in GoldRank.Items)
                {
                    Json.Value.Object goldRankItem = new Json.Value.Object()
                    {
                        { "uid", item.Uid },
                        { "uname", item.Name },
                        { "face", item.Face },
                        { "rank", item.UserRank },
                        { "score", item.Score },
                    };
                    goldRank.Add(goldRankItem);
                }
            }

            Json.Value.Array onlineRank = new Json.Value.Array();
            if (Rank != null)
            {
                foreach (OnlineRankItem item in Rank.Items)
                {
                    Json.Value.Object rankItem = new Json.Value.Object()
                    {
                        { "uid", item.Uid },
                        { "uname", item.Name },
                        { "face", item.Face },
                    };
                    onlineRank.Add(rankItem);
                }
            }

            Json.Value.Object resJson = new Json.Value.Object()
            {
                { "number", Rank.OnlineNum },
                { "gold_rank", goldRank },
                { "online_rank", onlineRank },
            };

            byte[] buffer = Encoding.UTF8.GetBytes(resJson.ToString());
            res.ContentType = "application/json";
            res.ContentEncoding = Encoding.UTF8;
            res.ContentLength64 = buffer.LongLength;

            res.OutputStream.Write(buffer, 0, buffer.Length);
            res.Close();
        }
    }
}
