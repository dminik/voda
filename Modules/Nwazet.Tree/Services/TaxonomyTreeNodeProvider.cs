using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Contrib.Taxonomies.Services;
using Orchard.ContentManagement;
using Orchard.Core.Containers.Models;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc.Html;

namespace Nwazet.Tree.Services {
    [OrchardFeature("Nwazet.Tree.TaxonomyBranches")]
    public class TaxonomyTreeNodeProvider : ITreeNodeProvider {
        private readonly ITaxonomyService _taxonomyService;
        private readonly IContentManager _contentManager;
        private readonly UrlHelper _url;

        public TaxonomyTreeNodeProvider(
            ITaxonomyService taxonomyService,
            IContentManager contentManager,
            UrlHelper urlHelper) {

            _taxonomyService = taxonomyService;
            _contentManager = contentManager;
            _url = urlHelper;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public IEnumerable<TreeNode> GetChildren(string nodeType, string nodeId) {
            switch (nodeType) {
                case "root":
                    return new[] {
                        new TreeNode {
                            Title = T("Taxonomies").Text,
                            Type = "taxonomies",
                            Id = "taxonomies"
                        }
                    };
                case "taxonomies":
                    return _taxonomyService.GetTaxonomies()
                        .Select(
                            t => new TreeNode {
                                Title = t.Name,
                                Type = "taxonomy",
                                Id = t.Id.ToString(CultureInfo.InvariantCulture),
                                Url = _url.Action(
                                    "Index", "TermAdmin",
                                    new RouteValueDictionary {
                                        {"area", "Contrib.Taxonomies"},
                                        {"taxonomyId", t.Id}
                                    }
                                     )
                            })
                        .OrderBy(n => n.Title)
                        .ToList();
                case "taxonomy":
                    var taxonomyId = int.Parse(nodeId);
                    return _taxonomyService.GetTerms(taxonomyId)
                        .Where(t => t.Path.Trim('/') == "")
                        .Select(
                            t => new TreeNode {
                                Title = t.Name,
                                Type = "taxonomy-term",
                                Id = t.Id.ToString(CultureInfo.InvariantCulture),
                                Url = _url.ItemEditUrl(t)
                            }
                        )
                        .OrderBy(n => n.Title)
                        .ToList();
                case "taxonomy-term":
                    var termId = int.Parse(nodeId);
                    var term = _taxonomyService.GetTerm(termId);
                    var childTermNodes = _taxonomyService
                        .GetChildren(term)
                        .Select(t => new TreeNode {
                            Title = t.Name,
                            Type = "taxonomy-term",
                            Id = t.Id.ToString(CultureInfo.InvariantCulture),
                            Url = _url.ItemEditUrl(t)
                        })
                        .OrderBy(n => n.Title);
                    var childItems = _taxonomyService
                        .GetContentItems(term)
                        .Select(i => new TreeNode {
                            Title = _contentManager.GetItemMetadata(i).DisplayText,
                            Type = "content-item-" + nodeId,
                            Id = i.Id.ToString(CultureInfo.InvariantCulture),
                            Url = _url.ItemEditUrl(i),
                            IsLeaf = !i.Has<ContainerPart>()
                        })
                        .OrderBy(n => n.Title);
                    return childTermNodes
                        .Union(childItems)
                        
                        .ToList();
            }
            return new TreeNode[0];
        }
    }
}