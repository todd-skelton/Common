using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kloc.Common.Instagram.Service
{
    public class TagsSearchResultsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public IEnumerable<Tag> Data { get; set; }
    }
}
