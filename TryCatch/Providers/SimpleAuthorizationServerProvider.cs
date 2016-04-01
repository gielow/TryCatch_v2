using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using TryCatch.Data;

namespace TryCatch.Providers
{
    // Source: http://bitoftech.net/2014/06/01/token-based-authentication-asp-net-web-api-2-owin-asp-net-identity/
    // The code was modified to use IoC to get the repository
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        IRepository _repository;

        public SimpleAuthorizationServerProvider(IRepository repository)
        {
            _repository = repository;
        }
        
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            if (!_repository.Customers.Exists(u => u.Email == context.UserName && u.Password == context.Password))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }


            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);

        }
    }
}