using Newtonsoft.Json;

namespace Kloc.Common.Instagram.Service
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "profile_picture")]
        public string ProfilePicture { get; set; }
        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }
        [JsonProperty(PropertyName = "bio")]
        public string Bio { get; set; }
        [JsonProperty(PropertyName = "website")]
        public string Website { get; set; }
        [JsonProperty(PropertyName = "is_business")]
        public bool IsBusiness { get; set; }
    }
}
