using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace UsersCollectionAPI.Utils;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options, 
        ILoggerFactory logger, 
        UrlEncoder encoder, 
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    } 
    
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Missing Authorization Header");
        }

        AuthenticationHeaderValue authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

        if (authHeader.Scheme != "Basic")
        {
            return AuthenticateResult.Fail("Invalid Authorization Scheme");
        }

        string[] credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter!)).Split(':');
        string username = credentials[0];


        Claim[] claims = { new (ClaimTypes.Name, username) };
        ClaimsIdentity identity = new (claims, Scheme.Name);
        ClaimsPrincipal principal = new (identity);
        AuthenticationTicket ticket = new (principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
