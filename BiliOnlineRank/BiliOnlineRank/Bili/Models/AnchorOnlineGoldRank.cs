using JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bili.Models
{
    public class AnchorOnlineGoldRank
    {
        public ulong OnlineNum { get; set; }
        public ulong TotalScore { get; set; }
        public List<AnchorOnlineGoldRankItem> Items { get; set; }

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
