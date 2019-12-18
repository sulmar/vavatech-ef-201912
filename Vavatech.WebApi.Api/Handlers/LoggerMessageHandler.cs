using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Vavatech.WebApi.Api.Handlers
{

    public class LoggerMessageHandler : DelegatingHandler 
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Trace.WriteLine($"{request.Method} {request.RequestUri}");

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Trace.WriteLine($"{response.StatusCode} {response.ReasonPhrase}");

            return response;
        }
    }
}