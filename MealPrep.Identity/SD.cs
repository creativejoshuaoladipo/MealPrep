using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrep.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()

            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
              new ApiScope(name:"Mango",displayName: "Mango Sever"),
              new ApiScope(name:"write",displayName: "werite your data"),
              new ApiScope(name:"read",displayName: "read your data"),
              new ApiScope(name:"delete",displayName: "delete the data")
            };


        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                   ClientId = "Client",
                   ClientSecrets = { new Secret ("secret".Sha256())},
                   RedirectUris = { "http://localhost:49837" },
                  AllowedGrantTypes = GrantTypes.ClientCredentials,
                  AllowedScopes = {"read","write","profile"}

                },

                  new Client
                {
                   ClientId = "mango",
                   ClientSecrets = { new Secret ("secret".Sha256())},
                   RedirectUris = { "http://localhost:49837/ signin-oidc" },
                   PostLogoutRedirectUris = { "http://localhost:49837/ signout-callback-oidc" },
                  AllowedGrantTypes = GrantTypes.Code,
                  AllowedScopes = new List<string>
                  {
                      IdentityServerConstants.StandardScopes.OpenId,
                      IdentityServerConstants.StandardScopes.Profile,
                      IdentityServerConstants.StandardScopes.Email
                  }

                }


            };


    }
}
