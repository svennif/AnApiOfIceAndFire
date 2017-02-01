using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace AnApiOfIceAndFire.Data.DocumentDb
{
    /// <summary>
    /// A factory that only returns a <see cref="DocumentDbRepository"/>
    /// </summary>
    public class DocumentDbRepositoryFactory : IRepositoryFactory
    {
        private DocumentDbRepository _repository;
        private readonly IOptions<DobumentDbOptions> _options;

        public DocumentDbRepositoryFactory(IOptions<DobumentDbOptions> options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _options = options;
        }

        public async Task<IRepository> CreateAsync()
        {
            if (_repository == null)
            {
                _repository = new DocumentDbRepository(_options);
                await _repository.Initialize();
            }

            return _repository;
        }
    }
}