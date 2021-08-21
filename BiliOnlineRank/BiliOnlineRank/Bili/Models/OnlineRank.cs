using JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bili.Models
{
    /// <summary>
    /// Class <c>OnlineRank</c> models the 用户在线列表
    /// </summary>
    public class OnlineRank
    {
        /// <summary>
        /// Number of online users
        /// </summary>
        public ulong OnlineNum { get; set; }
        /// <summary>
        /// List of online users
        /// </summary>
        public List<OnlineRankItem> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json">json object</param>
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
