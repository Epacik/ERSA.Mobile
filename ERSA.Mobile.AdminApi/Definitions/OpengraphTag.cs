using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERSA.Mobile.AdminApi
{
    public struct OpengraphTag
    {
        public OpengraphTag(int? iD, int linkID, string tag, string content)
        {
            ID = iD;
            LinkID = linkID;
            Tag = tag;
            Content = content;
        }

        [JsonProperty("log_id")]
        public int? ID { get; set; }

        [JsonProperty("log_link_id")]
        public int LinkID { get; set; }

        [JsonProperty("log_tag")]
        public string Tag { get; set; }

        [JsonProperty("log_content")]
        public string Content { get; set; }
    }

    public struct OpengraphTagToAdd
    {
        public OpengraphTagToAdd(int linkID, string tag, string content)
        {
            LinkID = linkID;
            Tag = tag;
            Content = content;
        }

        [JsonProperty("id")]
        public int LinkID { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
