using JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bili.Models
{
    class RoomInfo
    {
        public uint RoomId { get; set; }
        public uint Uid { get; set; }
        public string Uname { get; set; }
        public string Title { get; set; }
        public string Face { get; set; }
        public string TryTime { get; set; }
        public int LiveStatus { get; set; }
        public string AreaV2Name { get; set; }
        public int AreaV2Id { get; set; }
        public int MasterLevel { get; set; }
        public int MasterLevelColor { get; set; }
        public int MasterScore { get; set; }
        public int MasterNextLevel { get; set; }
        public int MaxLevel { get; set; }
        public int FcNum { get; set; }
        public int Rcost { get; set; }
        public int MedalStatus { get; set; }
        public string MedalName { get; set; }
        public int MedalRenameStatus { get; set; }
        public int IsMedal { get; set; }
        public string FullText { get; set; }
        public int IdentifyStatus { get; set; }
        public int LockStatus { get; set; }
        public string LockTime { get; set; }
        public int OpenMedalLevel { get; set; }
        public int MasterNextLevelScore { get; set; }
        public int ParentId { get; set; }
        public string ParentName { get; set; }
        public int LockStatusV2 { get; set; }
        public int GuardCount { get; set; }
        public int HaveLive { get; set; }
        public int FansClub { get; set; }
        public string AnchorStreamH5Url { get; set; }
        public Json.Value GuardWarn { get; set; }
        public int JoinSlide { get; set; }
        public Json.Value HotRankInfo { get; set; }

        public RoomInfo(Json.Value json)
        {
            RoomId = json["room_id"];
            Uid = json["uid"];
            Uname = json["uname"];
            Title = json["title"];
            Face = json["face"];
            TryTime = json["try_time"];
            LiveStatus = json["live_status"];
            AreaV2Name = json["area_v2_name"];
            AreaV2Id = json["area_v2_id"];
            MasterLevel = json["master_level"];
            MasterLevelColor = json["master_level_color"];
            MasterScore = json["master_score"];
            MasterNextLevel = json["master_next_level"];
            MaxLevel = json["max_level"];
            FcNum = json["fc_num"];
            Rcost = json["rcost"];
            MedalStatus = json["medal_status"];
            MedalName = json["medal_name"];
            MedalRenameStatus = json["medal_rename_status"];
            IsMedal = json["is_medal"];
            FullText = json["full_text"];
            IdentifyStatus = json["identify_status"];
            LockStatus = json["lock_status"];
            LockTime = json["lock_time"];
            OpenMedalLevel = json["open_medal_level"];
            MasterNextLevelScore = json["master_next_level_score"];
            ParentId = json["parent_id"];
            ParentName = json["parent_name"];
            LockStatusV2 = json["lock_status_v2"];
            GuardCount = json["guard_count"];
            HaveLive = json["have_live"];
            FansClub = json["fans_club"];
            AnchorStreamH5Url = json["anchor_stream_h5_url"];
            GuardWarn = json["guard_warn"];
            JoinSlide = json["join_slide"];
            HotRankInfo = json["hot_rank_info"];
        }
    }
}
