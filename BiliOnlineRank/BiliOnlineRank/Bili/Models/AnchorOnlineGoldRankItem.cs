using JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bili.Models
{
    /// <summary>
    /// Class <c>AnchorOnlineGoldRankItem</c> models an user in the 用户高能榜
    /// </summary>
    public class AnchorOnlineGoldRankItem
    {
        /// <summary>
        /// Rank number
        /// </summary>
        public uint UserRank { get; set; }
        /// <summary>
        /// User's UID
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
        /// Score number
        /// </summary>
        public uint Score { get; set; }
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
        public AnchorOnlineGoldRankItem(Json.Value json)
        {
            UserRank = json["userRank"];
            Uid = json["uid"];
            Name = json["name"];
            Face = json["face"];
            Score = json["score"];
            MedalInfo = json["medalInfo"];
            GuardLevel = json["guard_level"];
        }
    }
}
