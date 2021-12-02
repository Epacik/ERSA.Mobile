using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ERSA.Mobile.AdminApi
{
    public struct Link
    {
        [JsonProperty("lnk_id")]
        public int? Id { get; set; }

        [JsonProperty("lnk_path")]
        public string Path { get; set; }

        [JsonProperty("lnk_target")]
        public string Target { get; set; }

        [JsonProperty("lnk_hide_target")]
        public int HideTarget { get; set; }

    }
}
