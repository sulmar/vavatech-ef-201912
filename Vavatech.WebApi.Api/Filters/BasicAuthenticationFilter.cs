using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.Api.Filters
{
   

    public class BasicAuthenticationFilter : IAuthenticationFilter
    {
        private readonly ICustomerService customerService;

        public BasicAuthenticationFilter(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var authorization = context.Request.Headers.Authorization;

            if (authorization == null)
                return;

            if (authorization.Scheme != "Basic")
                return;

            byte[] credentialBytes =  Convert.FromBase64String(authorization.Parameter);

            string value = System.Text.Encoding.ASCII.GetString(credentialBytes);

            string[] credentials = value.Split(':');

            string username = credentials[0];
            string hashPassword = credentials[1];

            if (customerService.TryAuthorize(username, hashPassword, out Customer customer))
            {
                // TODO: from database
                string[] roles = new string[] { "admin", "developer" };

                Claim[] claims = new Claim[]
                {
                    new Claim(ClaimTypes.DateOfBirth, customer.DateOfBirth.ToString()),

                    new Claim(ClaimTypes.Email, "marcin.sulecki@sulmar.pl"),
                    new Claim(ClaimTypes.MobilePhone, "555-555-122"),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim(ClaimTypes.Role, "developer")
                };

                // IIdentity identity = new GenericIdentity(customer.FirstName, "Basic");
                // IPrincipal principal = new GenericPrincipal(identity, roles);

                IIdentity identity = new ClaimsIdentity(claims, "Basic");
                IPrincipal principal = new ClaimsPrincipal(identity);

                context.Principal = principal;
            }
            else
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

                response.ReasonPhrase = "Invalid username or password";

                context.ErrorResult = new ResponseMessageResult(response);

                return;
            }

        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(new System.Net.Http.Headers.AuthenticationHeaderValue("Basic"));
            }

            context.Result = new ResponseMessageResult(result);
        }
    }
}