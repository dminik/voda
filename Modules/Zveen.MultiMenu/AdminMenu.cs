using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.UI.Navigation;
using Orchard.Localization;
using Zveen.MultiMenu.Services;
using System.Diagnostics;

namespace Zveen.MultiMenu
{
	public class AdminMenu : INavigationProvider
	{
        private readonly IMultiMenuService _multiMenuService;

        public AdminMenu(IMultiMenuService multiMenuService)
        {
            _multiMenuService = multiMenuService;
        }

		public Localizer T { get; set; }

		public string MenuName { get { return "admin"; } }

		public void GetNavigation(NavigationBuilder builder)
		{
            builder.AddImageSet("multi-menu").Add(T("Multi Menu"), "5", menu =>
                menu.Add(T("Create New"), "5.1", item => item.Action("Create", "MultiMenuAdmin", new { area = "Zveen.MultiMenu" })));

			Int32 i = 2;
            foreach (var menuPart in _multiMenuService.FetchMenus())
            {
                builder.Add(T("Multi Menu"), "5", menu =>
                    menu.Add(T(menuPart.Title), "5." + i++.ToString(), item => item.Action("List", "MultiMenuAdmin", new { area = "Zveen.MultiMenu", idMultiMenu = menuPart.Id })));
            }

            
		}

		
	}
}