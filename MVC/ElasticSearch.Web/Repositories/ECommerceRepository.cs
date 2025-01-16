using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.Web.Models;
using ElasticSearch.Web.ViewModel;

namespace ElasticSearch.Web.Repositories
{
    public class ECommerceRepository
    {
        private readonly ElasticsearchClient _elasticsearchClient;
        private const string indexName = "kibana_sample_data_ecommerce";

        public ECommerceRepository(ElasticsearchClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }


        public async Task<(List<ECommerce>, long totalCount)> SearchECommerceAsync(EcommerceSearchViewModel ecommerceSearchViewModel, int page, int pageSize)
        {


            List<Action<QueryDescriptor<ECommerce>>> listAction = new();

            if (ecommerceSearchViewModel is null)
            {
                listAction.Add((q => q.MatchAll(new MatchAllQuery())));
                return await CalculateAndPageResult(page, pageSize, listAction);
            }

            if (!string.IsNullOrEmpty(ecommerceSearchViewModel.CustomerFullName))
                listAction.Add((q) => q.Match(m => m
                                            .Field(f => f.CustomerFullName)
                                            .Query(ecommerceSearchViewModel.CustomerFullName)));

            if (!string.IsNullOrEmpty(ecommerceSearchViewModel.Category))
                listAction.Add((q) => q.Match(m => m
                                            .Field(f => f.Category)
                                            .Query(ecommerceSearchViewModel.Category)));

            if (!string.IsNullOrEmpty(ecommerceSearchViewModel.Gender))
                listAction.Add((q) => q.Term(t => t
                                            .Field(f => f.Gender)
                                            .Value(ecommerceSearchViewModel.Gender).CaseInsensitive()));

            if (ecommerceSearchViewModel.OrderDateStart.HasValue)
                listAction.Add((q) => q.Range(r => r
                                            .DateRange(dr => dr
                                                .Field(f=> f.OrderDate)
                                                .Gte(ecommerceSearchViewModel.OrderDateStart.Value))));

            if (ecommerceSearchViewModel.OrderDateEnd.HasValue)
                listAction.Add((q) => q.Range(r => r
                                            .DateRange(dr => dr
                                                .Field(f=> f.OrderDate)
                                                .Lte(ecommerceSearchViewModel.OrderDateEnd.Value))));

            if (!listAction.Any())
            {
                listAction.Add((q => q.MatchAll(new MatchAllQuery())));
            }

            return await CalculateAndPageResult(page, pageSize, listAction);
        }

        private async Task<(List<ECommerce>, long totalCount)> CalculateAndPageResult(int page, int pageSize, List<Action<QueryDescriptor<ECommerce>>> listAction)
        {
            var pageFrom = (page - 1) * pageSize;

            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName)
                        .Query(q => q.
                            Bool(b => b
                                .Must(listAction.ToArray())))
                        .Size(pageSize).From(pageFrom)
                        .Sort(s => s
                            .Doc(d => d
                                .Order(SortOrder.Desc))
                                    .Field(s => s.OrderDate)));

            foreach (var hit in result.Hits) hit.Source!.Id = hit.Id!;

            return (result.Documents.ToList(), result.Total);
        }
    }
}
