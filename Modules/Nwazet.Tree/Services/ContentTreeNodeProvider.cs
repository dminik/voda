using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Core.Containers.Models;
using Orchard.Core.Contents.Settings;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc.Html;

namespace Nwazet.Tree.Services {
    [OrchardFeature("Nwazet.Tree.ContentBranches")]
    public class ContentTreeNodeProvider : ITreeNodeProvider {
        private readonly IContentManager _contentManager;
        private readonly UrlHelper _url;

        public ContentTreeNodeProvider(IContentManager contentManager, UrlHelper urlHelper) {
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
                            Title = T("Content Types").Text,
                            Type = "content-types",
                            Id = "content-types"
                        }
                    };
                case "content-types":
                    return _contentManager.GetContentTypeDefinitions()
                        .Where(d => d.Settings.GetModel<ContentTypeSettings>().Creatable)
                        .OrderBy(d => d.DisplayName)
                        .Select(
                            d => new TreeNode {
                                Title = d.DisplayName,
                                Type = "content-type",
                                Id = d.Name,
                                Url = _url.Action(
                                    "List", "Admin",
                                    new RouteValueDictionary {
                                        {"area", "Contents"},
                                        {"model.Id", d.Name}
                                    })
                            });
                case "content-type":
                    return _contentManager
                        .Query(VersionOptions.Latest, nodeId)
                        .List()
                        .Where(i => !i.Has<CommonPart>() || i.As<CommonPart>().Container == null)
                        .Select(
                            i => new TreeNode {
                                Title = _contentManager.GetItemMetadata(i).DisplayText,
                                Type = "content-item-" + nodeId,
                                Id = i.Id.ToString(CultureInfo.InvariantCulture),
                                Url = _url.ItemEditUrl(i),
                                IsLeaf = !i.Has<ContainerPart>()
                            })
                        .OrderBy(i => i.Title);
            }
            if (nodeType.StartsWith("content-item-")) {
                var containerId = int.Parse(nodeId);
                return _contentManager
                    .Query<CommonPart, CommonPartRecord>(VersionOptions.Latest)
                    .Where(i => i.Container.Id == containerId)
                    .List()
                    .Select(
                        i => new TreeNode {
                            Title = _contentManager.GetItemMetadata(i).DisplayText,
                            Type = "content-item-" + nodeId,
                            Id = i.Id.ToString(CultureInfo.InvariantCulture),
                            Url = _url.ItemEditUrl(i),
                            IsLeaf = !i.Has<ContainerPart>()
                        })
                    .OrderBy(i => i.Title);
            }
            return new TreeNode[0];
        }
    }
}