using JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bili.Models
{
    /// <summary>
    /// Class <c>RoomInfo</c> models information of a live room
    /// </summary>
    class RoomInfo
    {
        /// <summary>
        /// Live room ID
        /// </summary>
        public uint RoomId { get; set; }
        /// <summary>
        /// Streamer's Uid
        /// </summary>
        public uint Uid { get; set; }
        /// <summary>
        /// Streamer's name
        /// </summary>
        public string Uname { get; set; }
        /// <summary>
        /// Live room title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Url to the streamer's avatar image
        /// </summary>
        public string Face { get; set; }
        /// <summary>
        /// TBC
        /// </summary>
        public string TryTime { get; set; }
        /// <summary>
        /// Flag of live status
        /// </summary>
        public int LiveStatus { get; set; }
        /// <summary>
        /// Name of live area v2
        /// </summary>
        public string AreaV2Name { get; set; }
        /// <summary>
        /// ID of live area v2
        /// </summary>
        public int AreaV2Id { get; set; }
        /// <summary>
        /// Streamer's master level
        /// </summary>
        public int MasterLevel { get; set; }
        /// <summary>
        /// The color code of streamer's master level
        /// </summary>
        public int MasterLevelColor { get; set; }
        /// <summary>
        /// The score of streamer's master level
        /// </summary>
        public int MasterScore { get; set; }
        /// <summary>
        /// The streamer's next master level
        /// </summary>
        public int MasterNextLevel { get; set; }
        /// <summary>
        /// Max value of master level
        /// </summary>
        public int MaxLevel { get; set; }
        /// <summary>
        /// Number of followers
        /// </summary>
        public int FcNum { get; set; }
        /// <summary>
        /// TBC
        /// </summary>
        public int Rcost { get; set; }
        /// <summary>
        /// Flag for medal status
        /// </summary>
        public int MedalStatus { get; set; }
        /// <summary>
        /// Name of the medal
        /// </summary>
        public string MedalName { get; set; }
        /// <summary>
        /// Flag for medal rename status
        /// </summary>
        public int MedalRenameStatus { get; set; }
        /// <summary>
        /// Flag for medal activation status
        /// </summary>
        public int IsMedal { get; set; }
        /// <summary>
        /// TBC
        /// </summary>
        public string FullText { get; set; }
        /// <summary>
        /// TBC
        /// </summary>
        public int IdentifyStatus { get; set; }
        /// <summary>
        /// Flag for lock status of the live room
        /// </summary>
        public int LockStatus { get; set; }
        /// <summary>
        /// Flag for lock duration of the live room
        /// </summary>
        public string LockTime { get; set; }
        /// <summary>
        /// The level required to activate medals
        /// </summary>
        public int OpenMedalLevel { get; set; }
        /// <summary>
        /// Scores to be achieved for the streamer to reach the next master level
        /// </summary>
        public int MasterNextLevelScore { get; set; }
        /// <summary>
        /// ID of parent live area v2
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// Name of parent live area v2
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// Flag for lock status of the live room v2
        /// </summary>
        public int LockStatusV2 { get; set; }
        /// <summary>
        /// Number of guards
        /// </summary>
        public int GuardCount { get; set; }
        /// <summary>
        /// TBC
        /// </summary>
        public int HaveLive { get; set; }
        /// <summary>
        /// Flag for fans club activation status
        /// </summary>
        public int FansClub { get; set; }
        /// <summary>
        /// Url of live room popup
        /// </summary>
        public string AnchorStreamH5Url { get; set; }
        /// <summary>
        /// Guard expiration reminder
        /// </summary>
        public Json.Value GuardWarn { get; set; }
        /// <summary>
        /// TBC
        /// </summary>
        public int JoinSlide { get; set; }
        /// <summary>
        /// Information of hot rank related to the current live room 
        /// </summary>
        public Json.Value HotRankInfo { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="json">json object</param>
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
