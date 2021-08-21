using JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bili.Models
{
    /// <summary>
    /// Class <c>AnchorOnlineGoldRank</c> models the 用户高能榜
    /// </summary>
    public class AnchorOnlineGoldRank
    {
        /// <summary>
        /// Number of users in the rank
        /// </summary>
        public ulong OnlineNum { get; set; }
        /// <summary>
        /// Total score of the streamer
        /// </summary>
        public ulong TotalScore { get; set; }
        /// <summary>
        /// List of users in the rank
        /// </summary>
        public List<AnchorOnlineGoldRankItem> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json">Json object</param>
        public AnchorOnlineGoldRank(Json.Value json)
        {
            OnlineNum = json["onlineNum"];
            TotalScore = json["totalScore"];

            Items = new List<AnchorOnlineGoldRankItem>();
            foreach (Json.Value item in json["OnlineRankItem"])
            {
                AnchorOnlineGoldRankItem anchorOnlineGoldRankItem = new AnchorOnlineGoldRankItem(item);
                Items.Add(anchorOnlineGoldRankItem);
            }
        }
    }
}
