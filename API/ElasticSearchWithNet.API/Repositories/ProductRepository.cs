﻿using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearchWithNet.API.Models;
using System.Collections.Immutable;

namespace ElasticSearchWithNet.API.Repositories
{
    public class ProductRepository
    {
        private readonly ElasticsearchClient _client;
        private const string indexName = "products";
        public ProductRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<Product?> SaveAsync(Product product)
        {
            product.Created = DateTime.Now;

            var response = await _client.IndexAsync(product, x => x.Index(indexName).Id(Guid.NewGuid().ToString()));
            if (!response.IsSuccess()) return null;
            product.Id = response.Id;
            return product;
        }

        public async Task<ImmutableList<Product>> GetAllAsync()
        {
            var response = await _client.SearchAsync<Product>(s => s
            .Index(indexName)
            .Query(q => q
            .MatchAll(new MatchAllQuery())));

            foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;

            return response.Documents.ToImmutableList();
        }
    }
}
