using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Vavatech.WebApi.Api.Handlers
{
    public class SecretKeyMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.TryGetValues("secret-key", out IEnumerable<string> values))
            {
                return base.SendAsync(request, cancellationToken);
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

                return Task.FromResult(response);
            }

            

           
        }
    }
}