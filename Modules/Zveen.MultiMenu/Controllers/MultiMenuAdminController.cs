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

namespace Zveen.MultiMenu.Controllers
{
	[Admin]
	public class MultiMenuAdminController : Controller, IUpdateModel
	{
		public Localizer T { get; set; }
		public IOrchardServices Services { get; set; }
		private dynamic Shape { get; set; }

		private readonly IContentManager _contentManager;
		private readonly IMultiMenuService _multiMenuService;
		private readonly IMultiMenuItemService _multiMenuItemService;

		public MultiMenuAdminController(
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

        public ActionResult List(int idMultiMenu)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageMultiMenu, T("Not allowed to manage the menu")))
                return new HttpUnauthorizedResult();

            MultiMenuViewModel mmvm = new MultiMenuViewModel(); 
            mmvm.MenuItemEntries = _multiMenuItemService.Fetch(_multiMenuService.GetMenu(idMultiMenu)).Select( x => new MultiMenuItemViewModel(x) ).ToList();
            mmvm.MultiMenu = _multiMenuService.GetMenu(idMultiMenu);

            return View("List", mmvm);
        }

        [HttpPost]
        public ActionResult UpdateAll(int idMultiMenu, MultiMenuViewModel mmvm)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageMultiMenu, T("Not allowed to manage the menu")))
                return new HttpUnauthorizedResult();

            if(ModelState.IsValid == false)
                return View("List", mmvm);

            foreach (var item in mmvm.MenuItemEntries)
            {
                _multiMenuItemService.Update(item);
            }

            return List(idMultiMenu);
        }

		public ActionResult Create()
		{
            return View();
		}

        [HttpPost]
        public ActionResult CreateMultiMenuItem(int idMultiMenu, FormCollection part)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageMultiMenu, T("Not allowed to manage the menu")))
                return new HttpUnauthorizedResult();
            var menu = _multiMenuService.GetMenu(idMultiMenu);

            var item = _contentManager.New<MultiMenuItemPart>("ZveenMultiMenuItem");
            item.Title = part["NewMenuItem.Title"];
            item.Position = part["NewMenuItem.Position"];
            item.Url = part["NewMenuItem.Url"];
            item.As<ICommonPart>().Container = menu;

            if (!ModelState.IsValid)
            {
                Services.TransactionManager.Cancel();
                return View("List");
            }
            
            _contentManager.Create(item);

            return Redirect(Url.MultiMenuAdminList(idMultiMenu));
        }

        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int idMultiMenu)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageMultiMenu, T("Not allowed to manage the menu")))
                return new HttpUnauthorizedResult();

            var item = _multiMenuItemService.Get(id);
            _contentManager.Remove(item.ContentItem);

            return Redirect(Url.MultiMenuAdminList(idMultiMenu));
        }

        //public ActionResult Edit(Int32 idMultiMenu )
        //{
        //    if (!Services.Authorizer.Authorize(Permissions.ManageMultiMenu, T("Not allowed to edit multi menu")))
        //        return new HttpUnauthorizedResult();

        //    var multiMenuPart = _multiMenuService.GetMenu(idMultiMenu);
        //    var multiMenuItemParts = _multiMenuItemService.Fetch(idMultiMenu);

        //    dynamic menuShape = Services.ContentManager.BuildDisplay(multiMenuPart, "DisplayAdmin");
        //    dynamic menuItemShapes = multiMenuItemParts.Select(mmi => _contentManager.BuildDisplay(mmi, "SummaryAdmin"));

        //    var list = Services.New.List();
        //    list.AddRange(menuItemShapes);

        //    menuShape.Content.Add(Shape.MultiMenuWidget_MultiMenuItem_ListAdmin(ContentItems: list), "5");

        //    return View((object)menuShape);
        //}

        public void AddModelError(string key, Orchard.Localization.LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }

        public new bool TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) where TModel : class
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

	}
}