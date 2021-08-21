using JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bili.Models
{
    /// <summary>
    /// Class <c>OnlineRank</c> models an user in the 用户在线列表
    /// </summary>
    public class OnlineRankItem
    {
        /// <summary>
        /// User's ID
        /// </summary>
        public uint Uid { get; set; }
        /// <summary>
        /// User's name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Url to the user's avatar image
        /// </summary>
        public string Face { get; set; }
        /// <summary>
        /// Flag of continue watching
        /// </summary>
        public uint ContinueWatch { get; set; }
        /// <summary>
        /// Preserved
        /// </summary>
        public Json.Value MedalInfo { get; set; }
        /// <summary>
        /// Guard level
        /// </summary>
        public uint GuardLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json">json object</param>
        public OnlineRankItem(Json.Value json)
        {
            Uid = json["uid"];
            Name = json["name"];
            Face = json["face"];
            ContinueWatch = json["continueWatch"];
            MedalInfo = json["medalInfo"];
            GuardLevel = json["guardLevel"];
        }
    }
}
