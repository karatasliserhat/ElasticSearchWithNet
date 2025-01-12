using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ElasticSearch.Web.Repositories;
using ElasticSearch.Web.Services;
using System.Reflection;

namespace ElasticSearch.Web.Extensions
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

            Services.AddScoped<BlogRepository>();
            Services.AddScoped<BlogService>();


            Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
