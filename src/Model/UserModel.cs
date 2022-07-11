using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace AzureService.Model
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class UserModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}