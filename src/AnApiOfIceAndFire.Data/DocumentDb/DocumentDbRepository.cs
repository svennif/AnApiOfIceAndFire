using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Entities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.DocumentDb
{
    public class DocumentDbRepository : IRepository
    {
        private readonly IOptions<DobumentDbOptions> _options;
        private static DocumentClient _client;

        internal DocumentDbRepository(IOptions<DobumentDbOptions> options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _options = options;
            _client = new DocumentClient(new Uri(_options.Value.EndpointUri), _options.Value.PrimaryKey);
        }

        internal async Task Initialize()
        {
            var options = _options.Value;
            
            await _client.OpenAsync();
            await CreateDatabaseIfNotExists(_options.Value.DatabaseName);

            var createBooksCollection = CreateDocumentCollectionIfNotExists(options.DatabaseName, options.BookCollectionName, options.Throughput);
            var createCharactersCollection = CreateDocumentCollectionIfNotExists(options.DatabaseName, options.CharacterCollectionName, options.Throughput);
            var createHousesCollection = CreateDocumentCollectionIfNotExists(options.DatabaseName, options.HouseCollectionName, options.Throughput);

            await Task.WhenAll(createBooksCollection, createCharactersCollection, createHousesCollection);
        }

        public Task<TEntity> GetAsync<TEntity>(int id) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<TEntity>> GetPageAsync<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFilter, int pageNumber, int pageSize) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task UpsertAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }
        
        private static async Task CreateDatabaseIfNotExists(string databaseName)
        {
            try
            {
                await _client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    await _client.CreateDatabaseAsync(new Database { Id = databaseName });
                }
                else
                {
                    throw;
                }
            }
        }

        private static async Task CreateDocumentCollectionIfNotExists(string databaseName, string collectionName, int throughput)
        {
            try
            {
                await _client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    await _client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(databaseName),
                        new DocumentCollection { Id = collectionName }, new RequestOptions { OfferThroughput = throughput });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}