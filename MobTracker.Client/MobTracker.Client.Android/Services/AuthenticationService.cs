using Auth0.OidcClient;
using MobTracker.Client.Config;
using MobTracker.Client.Droid.Services;
using MobTracker.Client.Services.Interfaces;
using MobTracker.Client.Model;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthenticationService))]
namespace MobTracker.Client.Droid.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Auth0Client _auth0Client = new Auth0Client(new Auth0ClientOptions
        {
            Domain = AuthenticationConfig.Domain,
            ClientId = AuthenticationConfig.ClientId
        });

        public bool IsAuthenticated { get; private set; } = false;

        public AuthenticationResult AuthenticationResult { get; private set; }

        public async Task<AuthenticationResult> Authenticate()
        {
            var auth0LoginResult = await _auth0Client.LoginAsync(new { audience = AuthenticationConfig.Audience });
            AuthenticationResult authenticationResult;

            if (!auth0LoginResult.IsError)
            {
                IsAuthenticated = true;

                authenticationResult = new AuthenticationResult()
                {
                    AccessToken = auth0LoginResult.AccessToken,
                    IdToken = auth0LoginResult.IdentityToken,
                    UserClaims = auth0LoginResult.User.Claims
                };
            }
            else
            {
                authenticationResult = new AuthenticationResult(auth0LoginResult.IsError, auth0LoginResult.Error);
            }

            AuthenticationResult = authenticationResult;
            return authenticationResult;
        }
    }
}