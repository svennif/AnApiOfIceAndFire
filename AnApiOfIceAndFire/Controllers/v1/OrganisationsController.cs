using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Domain.Models.Filters;
using AnApiOfIceAndFire.Domain.Services;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models.v1;
using AnApiOfIceAndFire.Models.v1.Mappers;

namespace AnApiOfIceAndFire.Controllers.v1
{
    public class OrganisationsController : BaseController<IOrganisation, Organisation, OrganisationFilter>
    {
        public OrganisationsController(IModelService<IOrganisation, OrganisationFilter> modelService, IModelMapper<IOrganisation, Organisation> modelMapper, IPagingLinksFactory<OrganisationFilter> pagingLinksFactory) : base(modelService, modelMapper, pagingLinksFactory)
        {
        }

        [HttpHead]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, string name = "", string founded = "", string founder = "", bool? hasKnownMembers = null)
        {
            var organisationsFilter = new OrganisationFilter
            {
                Name = name,
                Founded = founded,
                Founder = founder,
                HasKnownMembers = hasKnownMembers
            };

            return await Get(page, pageSize, organisationsFilter);
        }
    }
}