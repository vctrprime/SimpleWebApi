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

        private const string HeaderName = "Authorization";
        
        public static readonly string SchemeName = nameof(BasicAuthenticationHandler).Replace("Handler", string.Empty);

        public static readonly string MissingHeaderMessage = "Missing Authorization Header";
        public static readonly string InvalidAuthorizationHeaderMessage = "Invalid Authorization Header";
        public static readonly string InvalidCredentialsMessage = "Invalid Username or Password";
        
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

            if (!Request.Headers.ContainsKey(HeaderName))
                return Fail(MissingHeaderMessage);

            bool isValidUser;
            Claim[] claims;;
            try
            {
                var (username, password) = DecodeAuthHeader();
                isValidUser = await _repository.IsValidUser(username, password);
                claims = new[] {
                    new Claim(ClaimTypes.Name, username),
                };
            }
            catch
            {
                return Fail(InvalidAuthorizationHeaderMessage);
            }

            if (!isValidUser)
                return Fail(InvalidCredentialsMessage);

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
        
        private (string username, string password) DecodeAuthHeader()
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers[HeaderName]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? string.Empty);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            var username = credentials[0];
            var password = credentials[1];

            return (username, password);
        }
        
        private AuthenticateResult Fail(string failReason)
        {
            _failReason = failReason;
            return AuthenticateResult.Fail(_failReason);
        }
    }
}