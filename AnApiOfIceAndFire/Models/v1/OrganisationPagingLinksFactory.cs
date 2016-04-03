using System;
using System.Collections.Generic;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models.Filters;
using AnApiOfIceAndFire.Infrastructure.Links;
using Geymsla.Collections;

namespace AnApiOfIceAndFire.Models.v1
{
    public class OrganisationPagingLinksFactory : IPagingLinksFactory<OrganisationFilter>
    {
        public IEnumerable<Link> Create<T>(IPagedList<T> pagedList, UrlHelper urlHelper, OrganisationFilter filter)
        {
            if (pagedList == null) throw new ArgumentNullException(nameof(pagedList));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var routeValues = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                routeValues.Add("name", filter.Name);
            }
            if (!string.IsNullOrEmpty(filter.Founded))
            {
                routeValues.Add("founded", filter.Founded);
            }
            if (!string.IsNullOrEmpty(filter.Founder))
            {
                routeValues.Add("founder", filter.Founder);
            }
            if (filter.HasKnownMembers.HasValue)
            {
                routeValues.Add("hasKnownMembers", filter.HasKnownMembers);
            }

            return pagedList.ToPagingLinks(urlHelper, OrganisationLinkCreator.OrganisationRouteName, routeValues);
        }
    }
}