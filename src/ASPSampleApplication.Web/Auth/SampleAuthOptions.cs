using Microsoft.AspNetCore.Authentication;

namespace ASPSampleApplication.Web.Auth
{
    public class SampleAuthOptions : AuthenticationSchemeOptions
    {
        public const string DefaultSchemeName = "SampleScheme";

        public string AuthToken { get; set; }
        public string HeaderName { get; set; }
    }
}
