using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace ASPSampleApplication.Web.Auth
{
    public class SampleTokenAuthHandler : AuthenticationHandler<SampleAuthOptions>
    {

        public SampleTokenAuthHandler(IOptionsMonitor<SampleAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }
    }
}
