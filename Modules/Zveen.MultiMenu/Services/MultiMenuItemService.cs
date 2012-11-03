using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;
using Zveen.MultiMenu.Models;
using Orchard.ContentManagement;
using Orchard.Core.Routable.Models;
using Orchard.Widgets.Models;
using Orchard.ContentManagement.Aspects;
using Orchard.Core.Common.Models;
using Orchard.UI;

namespace Zveen.MultiMenu.Services
{
	public interface IMultiMenuItemService : IDependency
	{
        IEnumerable<MultiMenuItemPart> Fetch(MultiMenuPart part);
        MultiMenuItemPart Get(int idMultiMenuItem);

        void Update(ViewModels.MultiMenuItemViewModel item);
    }

	public class MultiMenuItemService : IMultiMenuItemService
	{
		private readonly IContentManager _contentManager;

		public MultiMenuItemService(IContentManager contentManager)
		{
			_contentManager = contentManager;
		}

		public IEnumerable<MultiMenuItemPart> Fetch(MultiMenuPart part)
		{
            return _contentManager.Query("ZveenMultiMenuItem")
                .Join<CommonPartRecord>()
                .Where(x => x.Container == part.As<WidgetPart>().Record.ContentItemRecord)
                .List<MultiMenuItemPart>()
                .OrderBy(x => x.Position, new FlatPositionComparer());
		}
        public MultiMenuItemPart Get(int idMultiMenuItem)
        {
            return _contentManager.Get<MultiMenuItemPart>(idMultiMenuItem);
        }

        public void Update(ViewModels.MultiMenuItemViewModel item)
        {
            var menuItem = _contentManager.Get<MultiMenuItemPart>(item.Id);
            menuItem.Position = item.Position;
            menuItem.Title = item.Title;
            menuItem.Url = item.Url;

            _contentManager.Publish(menuItem.ContentItem);
        }
    }
}