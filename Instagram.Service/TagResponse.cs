using Newtonsoft.Json;

namespace Kloc.Common.Instagram.Service
{
    public class TagResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Tag Data { get; set; }
    }
}
