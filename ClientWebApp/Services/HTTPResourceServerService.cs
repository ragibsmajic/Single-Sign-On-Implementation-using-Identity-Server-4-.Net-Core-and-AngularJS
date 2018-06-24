using ClientWebApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientWebApp.Services
{
    public class HTTPResourceServerService : IHTTPResourceServerService
    {
        public async Task<string> GetUserClaimsAsync(string accessToken)
        {
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            
            var client = new HttpClient();

            client.SetBearerToken(accessToken);
            var content = await client.GetStringAsync("http://localhost:5001/identity");

            //return JsonConvert.DeserializeObject<IEnumerable<CustomClaim>>(content);

            return content;
        }

    }
}
