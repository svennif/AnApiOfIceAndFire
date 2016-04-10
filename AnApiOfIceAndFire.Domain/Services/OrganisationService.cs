using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Domain.Models.Filters;
using Geymsla;

namespace AnApiOfIceAndFire.Domain.Services
{
    public class OrganisationService : BaseService<IOrganisation, OrganisationEntity, OrganisationFilter>
    {
        private static readonly Expression<Func<OrganisationEntity, object>>[] OrganisationIncludeProperties =
      {
            organisation => organisation.Books,
            organisation => organisation.Founder,
            organisation => organisation.Members
        };


        public OrganisationService(IReadOnlyRepository<OrganisationEntity, int> repository, Expression<Func<OrganisationEntity, object>>[] includeProperties) : base(repository, includeProperties)
        {
        }

        protected override IOrganisation CreateModel(OrganisationEntity entity)
        {
            throw new NotImplementedException();
        }

        protected override Func<IQueryable<OrganisationEntity>, IQueryable<OrganisationEntity>> CreatePredicate(OrganisationFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
