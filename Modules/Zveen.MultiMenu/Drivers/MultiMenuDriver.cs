using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Drivers;
using Zveen.MultiMenu.Models;
using Orchard.ContentManagement;
using Zveen.MultiMenu.ViewModels;
using Orchard.Localization;
using Zveen.MultiMenu.Services;
using System.Diagnostics;

namespace Zveen.MultiMenu.Drivers
{
	public class MultiMenuDriver : ContentPartDriver<MultiMenuPart>
	{
        private readonly IMultiMenuItemService _multiMenuItemService;
        private readonly IContentManager _contentManager;

		public Localizer T { get; set; }

        public MultiMenuDriver(IMultiMenuItemService multiMenuItemService, IContentManager contentManager)
		{
            _multiMenuItemService = multiMenuItemService;
            _contentManager = contentManager;
		    T = NullLocalizer.Instance;
		}

        protected override DriverResult Display(MultiMenuPart part, string displayType, dynamic shapeHelper)
        {
            MultiMenuFrontendViewModel model = new MultiMenuFrontendViewModel(part,_multiMenuItemService.Fetch(part));
            return ContentShape("ZveenMultiMenu",
                () => shapeHelper.ZveenMultiMenu(
                    ContentItem: part.ContentItem,
                    Menu: model));
        }

		//GET
		protected override DriverResult Editor(MultiMenuPart part, dynamic shapeHelper)
		{
            return ContentShape("Parts_ZveenMultiMenu_Edit", () =>
				shapeHelper.EditorTemplate(
                    TemplateName: "Parts/ZveenParts_MultiMenu",
                    Model: part,
					Prefix: Prefix));
		}

		//POST
		protected override DriverResult Editor(MultiMenuPart part, IUpdateModel updater, dynamic shapeHelper)
		{
            updater.TryUpdateModel(part, Prefix, null, null);
			return Editor(part, shapeHelper);
		}
	}
}