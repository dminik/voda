using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.UI.Admin;
using System.Web.Mvc;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.DisplayManagement;
using Orchard;
using Zveen.MultiMenu.Services;
using Zveen.MultiMenu.Models;
using Zveen.MultiMenu.ViewModels;
using System.Diagnostics;
using Orchard.ContentManagement.Aspects;
using System.Text;

namespace Zveen.MultiMenu.Controllers
{
	public class MultiMenuController : Controller
	{
		public Localizer T { get; set; }
		public IOrchardServices Services { get; set; }
		private dynamic Shape { get; set; }

		private readonly IContentManager _contentManager;
		private readonly IMultiMenuService _multiMenuService;
		private readonly IMultiMenuItemService _multiMenuItemService;

        public MultiMenuController(
			IOrchardServices services,
			IContentManager contentManager, 
			IShapeFactory shapeFactory,
			IMultiMenuService multiMenuService,
			IMultiMenuItemService multiMenuItemService)
		{
			Services = services;
            _contentManager = contentManager;
            T = NullLocalizer.Instance;
            Shape = shapeFactory;
			_multiMenuService = multiMenuService;
			_multiMenuItemService = multiMenuItemService;
        }


        public ActionResult superfishCss(int id)
        {
            String text = System.IO.File.ReadAllText(Server.MapPath("~/Modules/Zveen.MultiMenu/Styles/superfish.css"));
            MultiMenuPart part = _multiMenuService.GetMenu(id);
            String Id = part.Title.Replace(' ', '_').Replace('-', '_');
            text = text.Replace("{Id}", Id);
            return File(Encoding.ASCII.GetBytes(text), "text/css");
        }

        public ActionResult superfishVerticalCss(int id)
        {
            String text = System.IO.File.ReadAllText(Server.MapPath("~/Modules/Zveen.MultiMenu/Styles/superfish-vertical.css"));
            MultiMenuPart part = _multiMenuService.GetMenu(id);
            String Id = part.Title.Replace(' ', '_').Replace('-', '_');
            text = text.Replace("{Id}", Id);
            return File(Encoding.ASCII.GetBytes(text), "text/css");
        }
        public ActionResult style(int id)
        {
            MultiMenuPart part = _multiMenuService.GetMenu(id);
            return File(Encoding.ASCII.GetBytes(part.Style), "text/css");
        }
	}
}