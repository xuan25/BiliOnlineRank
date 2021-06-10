using JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bili.Models
{
    public class OnlineRank
    {
        public ulong OnlineNum { get; set; }
        public List<OnlineRankItem> Items { get; set; }

        public OnlineRank(Json.Value json)
        {
            OnlineNum = json["onlineNum"];

            Items = new List<OnlineRankItem>();
            foreach (Json.Value item in json["item"])
            {
                OnlineRankItem onlineRankItem = new OnlineRankItem(item);
                Items.Add(onlineRankItem);
            }
        }
    }
}
