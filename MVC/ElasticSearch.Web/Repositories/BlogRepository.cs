﻿using Elastic.Clients.Elasticsearch;
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
    }
}
