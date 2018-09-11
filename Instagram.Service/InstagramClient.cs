using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kloc.Common.Instagram.Service
{
    public class InstagramClient
    {
        private readonly HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://api.instagram.com/v1/"),
        };

        public async Task<AccessTokenResponse> GetAccessToken(AccessTokenRequest request)
        {
            var form = new Dictionary<string, string>()
            {
                { "client_id", request.ClientId },
                { "client_secret", request.ClientSecret },
                { "grant_type", request.GrantType },
                { "redirect_uri", request.RedirectUri },
                { "code", request.Code }
            };

            var message = new HttpRequestMessage(HttpMethod.Post, "oauth/access_token")
            {
                Content = new FormUrlEncodedContent(form)
            };

            var response = await client.SendAsync(message);

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AccessTokenResponse>(content);

            return result;
        }

        public async Task<TagResponse> GetTagAsync(TagRequest request)
        {
            var response = await client.GetAsync($"tags/{request.TagName}?access_token={request.AccessToken}");

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TagResponse>(content);

            return result;
        }

        public async Task<TagsSearchResultsResponse> GetTagsSearchResultsAsync(TagsSearchResultsRequest request)
        {
            var response = await client.GetAsync($"tags/search?q={request.Query}&access_token={request.AccessToken}");

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TagsSearchResultsResponse>(content);

            return result;
        }
    }
}
