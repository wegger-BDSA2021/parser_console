using System;
using System.Net.Http;
using System.Threading.Tasks;
using static Parser.Chain.ResponseCodes;

namespace Parser.Chain
{
    class HttpResponseHandler : AbstractHandler
    {
        public override async Task<object> Handle(object request)
        {
            Uri uriResult; 
            bool isValid = Uri.TryCreate(request as string, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isValid)
            {
                throw new Exception();
                // return InvalidURL;
            }

            var host = uriResult.Host;

            var client = new HttpClient();
            var response = await client.GetAsync(request as string); 

            var httpStatusCode = response.StatusCode;
            if (httpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception();
            }

            var content = await response.Content.ReadAsStringAsync();
            return base.Handle(content);
        }
    }
}