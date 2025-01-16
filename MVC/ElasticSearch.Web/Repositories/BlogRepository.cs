using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.Web.Models;

namespace ElasticSearch.Web.Repositories
{
    public class BlogRepository
    {
        private readonly ElasticsearchClient _elasticsearchClient;
        private const string indexName = "blog";
        public BlogRepository(ElasticsearchClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }

        public async Task<Blog?> Save(Blog newBlog)
        {
            newBlog.Created = DateTime.Now;
            var response = await _elasticsearchClient.IndexAsync(newBlog, x => x.Index(indexName));
            if (!response.IsValidResponse) return null;

            newBlog.Id = response.Id;
            return newBlog;
        }

        public async Task<List<Blog>> SearchAsync(string searchText)
        {
            List<Action<QueryDescriptor<Blog>>> actions = new();

            Action<QueryDescriptor<Blog>> matAllQuery = q => q.MatchAll(new MatchAllQuery());

            Action<QueryDescriptor<Blog>> matcQueryContent = q => q.Match(m => m.Field(f => f.Content).Query(searchText));

            Action<QueryDescriptor<Blog>> matchBoolPrefixQueryTitle = q => q.MatchBoolPrefix(m => m.Field(f => f.Title).Query(searchText));

            Action<QueryDescriptor<Blog>> TermQeuryTags = q => q.Term(t => t.Field(f => f.Tags).Value(searchText));


            if (string.IsNullOrEmpty(searchText))
                actions.Add(matAllQuery);

            else
            {
                actions.Add(matcQueryContent);
                actions.Add(matchBoolPrefixQueryTitle);
                actions.Add(TermQeuryTags);
            }


            var response = await _elasticsearchClient.SearchAsync<Blog>(s => s.Index(indexName)
            .Query(q => q
                .Bool(b => b
                    .Should(actions.ToArray())))
            .Size(100));

            foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;

            return response.Documents.ToList();
        }
    }
}
