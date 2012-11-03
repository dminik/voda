using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nwazet.Tree.Services;
using Orchard;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Security;
using Orchard.Themes;
using Orchard.UI.Admin;

namespace Nwazet.Tree.Controllers {
    [Admin]
    [Themed(false)]
    [OrchardFeature("Nwazet.Tree")]
    public class TreeController : Controller {
        private readonly IEnumerable<ITreeNodeProvider> _treeNodeProviders;

        public IOrchardServices Services { get; set; }

        public TreeController(IEnumerable<ITreeNodeProvider> treeNodeProviders, IOrchardServices services) {
            _treeNodeProviders = treeNodeProviders;
            Services = services;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        [HttpPost]
        public ActionResult GetChildren(string parentId, string parentType) {
            var notAuthorized = T("Not authorized to access the admin panel.");
            if (!Services.Authorizer.Authorize(StandardPermissions.AccessAdminPanel, notAuthorized)) {
                return Json(new { Success = false, Message = notAuthorized.Text });
            }

            try {
                var children = _treeNodeProviders
                    .SelectMany(p => p.GetChildren(parentType, parentId));
                return Json(children);
            }
            catch (Exception exception) {
                return Json(new { Success = false, Message = T("Tree exapnsion failed: {0}", exception.Message).ToString() });
            }
        }
    }
}