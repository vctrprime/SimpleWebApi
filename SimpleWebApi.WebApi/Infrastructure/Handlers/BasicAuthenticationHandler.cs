using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleWebApi.DAL.Repositories.Abstract.Common;

namespace SimpleWebApi.WebApi.Infrastructure.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthorizationRepository _repository;
        private string _failReason;
        
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthorizationRepository repository)
            : base(options, logger, encoder, clock)
        {
            _repository = repository;
        }
        
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // skip authentication if endpoint has [AllowAnonymous] attribute
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                return Fail("Missing Authorization Header");

            bool isValidUser;
            string username;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                username = credentials[0];
                var password = credentials[1];
                isValidUser = await _repository.IsValidUser(username, password);
            }
            catch
            {
                return Fail("Invalid Authorization Header");
            }

            if (!isValidUser)
                return Fail("Invalid Username or Password");

            var claims = new[] {
                new Claim(ClaimTypes.Name, username),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 401;

            if (_failReason != null)
            {
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = _failReason;
            }

            return Task.CompletedTask;
        }

        private AuthenticateResult Fail(string failReason)
        {
            _failReason = failReason;
            return AuthenticateResult.Fail(_failReason);
        }
    }
}