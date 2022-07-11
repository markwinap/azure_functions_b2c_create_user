using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AzureService.Model
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ResponseModel
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}