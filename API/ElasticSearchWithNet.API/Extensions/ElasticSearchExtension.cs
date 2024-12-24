using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ElasticSearchWithNet.API.Repositories;
using ElasticSearchWithNet.API.Services;
using System.Reflection;

namespace ElasticSearchWithNet.API.Extensions
{
    public static class ElasticSearchExtension
    {
        public static void AddElasticSearchExtensionService(this IServiceCollection Services, IConfiguration Configuration)
        {

            var userName = Configuration.GetSection("Elastic")["UserName"];
            var password = Configuration.GetSection("Elastic")["Password"];
            var uri = Configuration.GetSection("Elastic")["Url"];

            var setting = new ElasticsearchClientSettings(new Uri(uri!)).Authentication(new BasicAuthentication(userName!, password!));

            var client = new ElasticsearchClient(setting);

            Services.AddSingleton(client);

            Services.AddScoped<ProductRepository>();
            Services.AddScoped<ProductService>();

            Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
