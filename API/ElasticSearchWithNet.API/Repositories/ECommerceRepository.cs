using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearchWithNet.API.EcommerceModels;
using System.Collections.Immutable;

namespace ElasticSearchWithNet.API.Repositories
{
    public class ECommerceRepository
    {
        private readonly ElasticsearchClient _client;
        private const string indexName = "kibana_sample_data_ecommerce";
        public ECommerceRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<ImmutableList<ECommerce>> TermQuery(string customerFirstName)
        {



            //1.yol
            //var result = await _client.SearchAsync<ECommerce>(q => q
            //.Index(indexName)
            //.Query(t => t
            //.Term(f => f
            //.Field(("customer_first_name.keyword")!)
            //.Value(customerFirstName)))
            //.Size(100));

            //2.yol
            //var result = await _client.SearchAsync<ECommerce>(s => s
            //.Index(indexName)
            //.Query(q => q
            //.Term(t => t
            //.Field(f => f
            //.CustomerFirstName.Suffix("keyword")).Value(customerFirstName))));

            //3.yol
            var termQuery = new TermQuery(("customer_first_name.keyword")!) { Value = customerFirstName, CaseInsensitive = true };

            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Query(q => q.Term(termQuery)));


            foreach (var hit in result.Hits) hit.Source!.Id = hit.Id!;
            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> TermsQuery(List<string> customerFirstNames)
        {
            List<FieldValue> terms = new List<FieldValue>();

            customerFirstNames.ForEach(term => terms.Add(term));
            //var termsQuery = new TermsQuery
            //{
            //    Field = ("customer_first_name.keyword")!,
            //    Terms = new TermsQueryField(terms.AsReadOnly())
            //};

            //var result = await _client.SearchAsync<ECommerce>(s => s
            //.Index(indexName)
            //.Query(q => q
            //.Terms(termsQuery))
            //.Size(100));

            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Query(q => q
            .Terms(t => t
            .Field(f => f
            .CustomerFirstName
            .Suffix("keyword"))
            .Terms(new TermsQueryField(terms.AsReadOnly())))));

            foreach (var hit in result.Hits) hit.Source!.Id = hit.Id!;

            return result.Documents.ToImmutableList();
        }
        public async Task<ImmutableList<ECommerce>> PrefixQuery(string customerFullName)
        {
            var response = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Query(q => q
            .Prefix(p => p
            .Field(f => f.CustomerFullName
            .Suffix("keyword"))
            .Value(customerFullName)))
            .Size(100));
            foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;

            return response.Documents.ToImmutableList();
        }
        public async Task<ImmutableList<ECommerce>> RangeQuery(double fromPrice, double toPrice)
        {
            var response = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Query(q => q
            .Range(r => r
            .NumberRange(nr => nr
            .Field(f => f
            .TaxFulTotalPrice)
            .Gte(fromPrice)
            .Lte(toPrice))))
            .Size(100));
            foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;

            return response.Documents.ToImmutableList();
        }
    }
}
