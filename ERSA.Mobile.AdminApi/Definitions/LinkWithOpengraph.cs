using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERSA.Mobile.AdminApi
{
    public class LinkWithOpengraph
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("hide_target")]
        public int HideTarget { get; set; }

        [JsonProperty("opengraph_tags")]
        public OpengraphTag[] OpengraphTags { get; set; }
    }
}
