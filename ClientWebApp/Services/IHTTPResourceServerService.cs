using ClientWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClientWebApp.Services
{
    public interface IHTTPResourceServerService
    {
        Task<string> GetUserClaimsAsync(string accessToken);
    }
}
