namespace Kloc.Common.Instagram.Service
{
    public class AccessTokenRequest
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string GrantType { get; set; }
        public string RedirectUri { get; set; }
        public string Code { get; set; }
    }
}
