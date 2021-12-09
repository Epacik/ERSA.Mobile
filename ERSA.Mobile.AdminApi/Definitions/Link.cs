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
        private int _hideTarget { get; set; }

        [JsonIgnore]
        public bool HideTarget { get => _hideTarget == 1; set => _hideTarget = value ? 1 : 0; }

        public static explicit operator Link((int? Id, string Path, string Target, bool HideTarget) l)
        {
            return new Link { Id = l.Id, Path = l.Path, Target = l.Target, HideTarget = l.HideTarget };
        }
        public static explicit operator Link((string Path, string Target, bool HideTarget) l)
        {
            return new Link { Id = null, Path = l.Path, Target = l.Target, HideTarget = l.HideTarget };
        }

    }

    public struct LinkToAdd
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("hide_target")]
        private int _hideTarget { get; set; }

        [JsonIgnore]
        public bool HideTarget { get => _hideTarget == 1; set => _hideTarget = value ? 1 : 0; }

        public static explicit operator LinkToAdd((string Path, string Target, bool HideTarget) l)
        {
            return new LinkToAdd { Path = l.Path, Target = l.Target, HideTarget = l.HideTarget };
        }
    }
}
