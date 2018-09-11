using Newtonsoft.Json;

namespace Kloc.Common.Instagram.Service
{
    public class AccessTokenResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }
    }
}
