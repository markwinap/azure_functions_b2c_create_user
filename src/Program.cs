using AzureService.Model;
using AzureService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Graph;
using Azure.Identity;

namespace AzureService
{
    public class Program
    {
        private readonly IIdentityRepository _identityRepository;

        public Program(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        [FunctionName("user")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            try
            {
                var config = new ConfigurationBuilder()
                        .SetBasePath(context.FunctionAppDirectory)
                        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();

                string requestBody = string.Empty;

                using (StreamReader streamReader = new StreamReader(req.Body))
                {
                    requestBody = await streamReader.ReadToEndAsync();
                }

                var setting = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
                };

                UserModel user = JsonConvert.DeserializeObject<UserModel>(requestBody, setting);

                var userResult = await _identityRepository.PostUser(user);

                return new OkObjectResult(userResult);
            }
            catch (Exception ex)
            {
                log.LogInformation("Exception - {0}", ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
