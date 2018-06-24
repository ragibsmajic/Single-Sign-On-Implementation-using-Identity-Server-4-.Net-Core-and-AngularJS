using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationServer
{
    public class ASConfig
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("company_photos_api_server", "Access to Company's Photos")
                {
                     UserClaims = {"role"}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // OpenID Connect code flow client (MVC) - Company's main web page
                new Client
                {
                    ClientId = "mvc_main_company_web_app",
                    ClientName = "Company's Main Web App",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    }
                },

                // OpenID Connect implicit flow client (AngularJS Client - Company's Photos App)
                new Client
                {
                    ClientId = "company_photos_web_app",
                    ClientName = "Company's Photos App",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:8303" },             
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:8303" },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "company_photos_api_server"
                    }
                },

                 new Client
                {
                    ClientId = "mvc2",
                    ClientName = "MVC WebApp Client2",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5003/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        ClaimTypes.Name,
                        ClaimTypes.Webpage,
                        ClaimTypes.Email,
                        ClaimTypes.NameIdentifier
                    }
                },

                new Client
                {
                    ClientId = "mvc_external_app",
                    ClientName = "External Web Application",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireConsent = true,
                    AllowOfflineAccess = true,
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5004/signin-oidc" },
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5004/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "rsmajic",
                    Password = "lozinka",
                    Claims = new []
                    {
                        new Claim("name", "Ragib Smajic"),
                        new Claim("website", "https://ragibsmajic.ba"),
                        new Claim("email", "rsmajic1@etf.unsa.ba"),
                        new Claim("role", "Administrator")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }
    }
}
