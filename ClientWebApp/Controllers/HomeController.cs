using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using ClientWebApp.Services;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using JWT;
using JWT.Serializers;

namespace ClientWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHTTPResourceServerService iHTTPResourceServerService;
        
        public HomeController(IHTTPResourceServerService iHTTPResourceServerService)
        {
            this.iHTTPResourceServerService = iHTTPResourceServerService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> Protected()
        {
            ViewData["Message"] = "Informacije koje je vratio AuthorizationServer: ";

            var access_token = await HttpContext.GetTokenAsync("access_token");

            //var response = await iHTTPResourceServerService.GetUserClaimsAsync(access_token);

            var access_token_json = "";
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                access_token_json = decoder.Decode(access_token);
                ViewData["access_token_decoded"] = access_token_json;

            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }

            //ViewData["response"] = response;
            
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }

        
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = "/" });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
