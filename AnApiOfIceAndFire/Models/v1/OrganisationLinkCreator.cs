using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Models.v1
{
    public static class OrganisationLinkCreator
    {
        public const string OrganisationRouteName = "OrganisationsApi";

        public static string CreateOrganisationLink(IOrganisation organisation, UrlHelper urlHelper)
        {
            if (organisation == null) return string.Empty;

            return urlHelper.Link(OrganisationRouteName, new { id = organisation.Identifier });
        }
    }
}