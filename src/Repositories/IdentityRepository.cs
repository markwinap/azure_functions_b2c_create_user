using System;
using System.Threading.Tasks;
using Azure.Identity;
using AzureService.Model;
using Microsoft.Graph;

namespace AzureService.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        public string appId;
        public string clientSecret;
        public string tenantId;
        public string issuer;


        public IdentityRepository()
        {
            appId = Environment.GetEnvironmentVariable("AZURE_APP_ID");
            clientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET");
            tenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID");
            issuer = Environment.GetEnvironmentVariable("AZURE_ISSUER");
        }


        public async Task<ResponseModel> PostUser(UserModel user)
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            var clientSecretCredential = new ClientSecretCredential(tenantId, appId, clientSecret, options);
            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

            var principalName = user.UserName + "@" + issuer;
            var password = Helpers.PasswordHelper.GenerateNewPassword(4, 8, 4);

            var newUser = new User
            {
                AccountEnabled = true,
                DisplayName = user.Name + " " + user.LastName,
                MailNickname = user.UserName,
                UserPrincipalName = principalName,
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = true,
                    Password = password
                }
            };
            var result = await graphClient.Users.Request().AddAsync(newUser);

            var response = new ResponseModel();

            response.Id = result.Id;
            response.Password = password;
            response.UserName = principalName;
            return response;
        }
    }
}
