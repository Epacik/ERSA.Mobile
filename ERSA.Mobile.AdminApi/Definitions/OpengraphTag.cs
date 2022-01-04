using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERSA.Mobile.AdminApi
{
    public struct OpengraphTag
    {
        [JsonProperty("log_id")]
        public int? ID { get; set; }

        [JsonProperty("log_link_id")]
        public int LinkID { get; set; }

        [JsonProperty("log_tag")]
        public string Tag { get; set; }

        [JsonProperty("log_content")]
        public string Content { get; set; }
    }
}
