using JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bili.Models
{
    public class OnlineRankItem
    {
        public uint Uid { get; set; }
        public string Name { get; set; }
        public string Face { get; set; }
        public uint ContinueWatch { get; set; }
        public Json.Value MedalInfo { get; set; }
        public uint GuardLevel { get; set; }

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
