using Bili;
using Bili.Models;
using JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bili
{
    /// <summary>
    /// Class <c>BiliApi</c> wraps common Api methods for bilibili live room.
    /// </summary>
    class BiliLive
    {

        /// <summary>
        /// Get room info
        /// </summary>
        /// <returns></returns>
        public static RoomInfo GetRoomInfo()
        {
            Dictionary<string, string> payload = new Dictionary<string, string>()
            {
                { "platform", "pc_link" },
            };

            string authUrl = "https://api.live.bilibili.com/xlive/app-blink/v1/room/GetInfo";
            Json.Value res = BiliApi.RequestJsonResult(authUrl, payload, false);

            switch((int)res["code"])
            {
                case 0:
                    RoomInfo roomInfo = new RoomInfo(res["data"]);
                    return roomInfo;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Get online rank (online users)
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="roomId">room id</param>
        /// <param name="ruid">user id</param>
        /// <returns>OnlineRank</returns>
        public static OnlineRank GetOnlineRank(string page, string pageSize, string roomId, string ruid)
        {
            Dictionary<string, string> payload = new Dictionary<string, string>()
            {
                { "page", page },
                { "pageSize", pageSize },
                { "roomId", roomId },
                { "ruid", ruid },
                { "platform", "pc_link" },
            };

            string authUrl = "https://api.live.bilibili.com/xlive/general-interface/v1/rank/getOnlineRank";
            Json.Value res = BiliApi.RequestJsonResult(authUrl, payload, false);

            switch ((int)res["code"])
            {
                case 0:
                    OnlineRank onlineRank = new OnlineRank(res["data"]);
                    return onlineRank;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Get anchor online gold rank (gold rank)
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="roomId">room id</param>
        /// <param name="ruid">user id</param>
        /// <returns>AnchorOnlineGoldRank</returns>
        public static AnchorOnlineGoldRank GetAnchorOnlineGoldRank(string page, string pageSize, string roomId, string ruid)
        {
            Dictionary<string, string> payload = new Dictionary<string, string>()
            {
                { "page", page },
                { "pageSize", pageSize },
                { "roomId", roomId },
                { "ruid", ruid },
                { "platform", "pc_link" },
            };

            string authUrl = "https://api.live.bilibili.com/xlive/general-interface/v1/rank/getAnchorOnlineGoldRank";
            Json.Value res = BiliApi.RequestJsonResult(authUrl, payload, false);

            switch ((int)res["code"])
            {
                case 0:
                    AnchorOnlineGoldRank anchorOnlineGoldRank = new AnchorOnlineGoldRank(res["data"]);
                    return anchorOnlineGoldRank;
                default:
                    return null;
            }
        }
    }
}
