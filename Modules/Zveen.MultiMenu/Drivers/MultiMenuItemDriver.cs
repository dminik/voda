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

namespace Zveen.MultiMenu.Drivers
{
	public class MultiMenuItemDriver : ContentPartDriver<MultiMenuItemPart>
	{

        protected override DriverResult Display(MultiMenuItemPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_MultiMenuItem", () =>
                    shapeHelper.Parts_MultiMenuItem(
                        ContentPart: part));
		}

		//GET
        protected override DriverResult Editor(MultiMenuItemPart part, dynamic shapeHelper)
		{
            return ContentShape("Parts_MultiMenuItem_Edit", () =>
				shapeHelper.EditorTemplate(
                    TemplateName: "Parts/MultiMenuItem",
                    Model: part,
					Prefix: Prefix));
		}

		//POST
        protected override DriverResult Editor(MultiMenuItemPart part, IUpdateModel updater, dynamic shapeHelper)
		{
            updater.TryUpdateModel(part, Prefix, null, null);
			return Editor(part, shapeHelper);
		}
	}
}