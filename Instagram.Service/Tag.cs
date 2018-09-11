using Newtonsoft.Json;

namespace Kloc.Common.Instagram.Service
{
    public class Tag
    {
        [JsonProperty(PropertyName = "media_count")]
        public int MediaCount { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
