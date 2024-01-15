using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Hypermedia.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ASPNETDockerRestAPI.Hypermedia.Enricher
{
    public class PersonEnricher : ContentResponseEnricher<PersonDto>
    {
        private readonly object _lock = new();

        protected override Task EnrichModel(PersonDto content, IUrlHelper urlHelper)
        {
            var path = "api/v1/persons";
            var link = GetLink(content.Id, urlHelper, path);

            content.Links.Add(new HypermediaLink
            {
                Action = HttpActionVerbs.GET,
                Href = link,
                Rel = RelationType.SELF,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HypermediaLink
            {
                Action = HttpActionVerbs.POST,
                Href = link,
                Rel = RelationType.SELF,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HypermediaLink
            {
                Action = HttpActionVerbs.PUT,
                Href = link,
                Rel = RelationType.SELF,
                Type = ResponseTypeFormat.DefaultPut
            });

            content.Links.Add(new HypermediaLink
            {
                Action = HttpActionVerbs.PATCH,
                Href = link,
                Rel = RelationType.SELF,
                Type = ResponseTypeFormat.DefaultPatch
            });

            content.Links.Add(new HypermediaLink
            {
                Action = HttpActionVerbs.DELETE,
                Href = link,
                Rel = RelationType.SELF,
                Type = "int"
            });

            return Task.CompletedTask;
        }

        private string GetLink(long id, IUrlHelper urlHelper, string path)
        {
            lock (_lock)
            {
                var url = new { controller = path, id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            }
        }
    }
}
