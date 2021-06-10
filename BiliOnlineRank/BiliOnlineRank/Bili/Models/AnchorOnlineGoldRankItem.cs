using JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bili.Models
{
    public class AnchorOnlineGoldRankItem
    {
        public uint userRank { get; set; }
        public uint Uid { get; set; }
        public string Name { get; set; }
        public string Face { get; set; }
        public uint Score { get; set; }
        public Json.Value MedalInfo { get; set; }
        public uint GuardLevel { get; set; }

        public AnchorOnlineGoldRankItem(Json.Value json)
        {
            userRank = json["userRank"];
            Uid = json["uid"];
            Name = json["name"];
            Face = json["face"];
            Score = json["score"];
            MedalInfo = json["medalInfo"];
            GuardLevel = json["guard_level"];
        }
    }
}
