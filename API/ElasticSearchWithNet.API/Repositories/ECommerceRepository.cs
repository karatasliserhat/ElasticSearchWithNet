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

        public async Task<ImmutableList<ECommerce>> TermQueryAsync(string customerFirstName)
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

        public async Task<ImmutableList<ECommerce>> TermsQueryAsync(List<string> customerFirstNames)
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
        public async Task<ImmutableList<ECommerce>> PrefixQueryAsync(string customerFullName)
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
        public async Task<ImmutableList<ECommerce>> RangeQueryAsync(double fromPrice, double toPrice)
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
        public async Task<ImmutableList<ECommerce>> MatchAllQueryAsync()
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Query(q => q
            .MatchAll(new MatchAllQuery()))
            .Size(100));
            foreach (var hit in result.Hits) hit.Source!.Id = hit.Id!;

            return result.Documents.ToImmutableList();

        }

        public async Task<ImmutableList<ECommerce>> PaginationQueryAsync(int page, int pageSize)
        {
            var pageFrom = (page - 1) * pageSize;
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Query(q => q
            .MatchAll(new MatchAllQuery()))
            .Size(pageSize).From(pageFrom));

            foreach (var hit in result.Hits) hit.Source!.Id = hit.Id!;

            return result.Documents.ToImmutableList();

        }

        public async Task<ImmutableList<ECommerce>> WildcardQueryAsync(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Query(q => q
            .Wildcard(w => w
            .Field(f => f.CustomerFullName
            .Suffix("keyword"))
            .Wildcard(customerFullName)))
            .Size(100));

            foreach (var hit in result.Hits) hit.Source!.Id = hit.Id!;
            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> FuzzyQueryAsync(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Query(q => q
            .Fuzzy(fu => fu
            .Field(f => f
            .CustomerFullName.Suffix("keyword"))
            .Value(customerFullName).Fuzziness(new Fuzziness(2))))
            .Size(100)
            .Sort(sort => sort
            .Field(f => f
            .TaxFulTotalPrice, new FieldSort { Order = SortOrder.Desc })));

            foreach (var hit in result.Hits) hit.Source!.Id = hit.Id!;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> MatchQueryTextFullAsync(string category)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Query(q => q
            .Match(m => m
            .Field(f => f.Category).Query(category)))
            .Size(100));
            foreach (var hit in result.Hits) hit.Source!.Id = hit.Id!;

            return result.Documents.ToImmutableList();

        }

        public async Task<ImmutableList<ECommerce>> MatchBoolPrefixQueryTextFullAsync(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Query(q => q
            .MatchBoolPrefix(m => m
            .Field(f => f.CustomerFullName).Query(customerFullName)))
            .Size(100));
            foreach (var hit in result.Hits) hit.Source!.Id = hit.Id!;

            return result.Documents.ToImmutableList();

        }

        public async Task<ImmutableList<ECommerce>> MatchPhraseQueryTextFullAsync(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Query(q => q
            .MatchPhrase(m => m
            .Field(f => f.CustomerFullName).Query(customerFullName)))
            .Size(100));
            foreach (var hit in result.Hits) hit.Source!.Id = hit.Id!;

            return result.Documents.ToImmutableList();

        }

    }
}
