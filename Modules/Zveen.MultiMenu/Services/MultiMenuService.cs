using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Zveen.MultiMenu.Models;
using Orchard;

namespace Zveen.MultiMenu.Services
{
    public interface IMultiMenuService : IDependency
    {
        IEnumerable<MultiMenuPart> FetchMenus();
        MultiMenuPart GetMenu(Int32 idMultiMenuPart);
    }

    public class MultiMenuService: IMultiMenuService
    {
        private readonly IContentManager _contentManager;

        public MultiMenuService(IContentManager contentManager)
		{
			_contentManager = contentManager;
		}


        public IEnumerable<MultiMenuPart> FetchMenus()
        {
            return _contentManager.Query<MultiMenuPart>().List();
        }

        public MultiMenuPart GetMenu(int idMultiMenuPart)
        {
            return _contentManager.Get<MultiMenuPart>(idMultiMenuPart);
        }

    }
}