using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Zveen.MultiMenu.Models;

namespace Zveen.MultiMenu.Handlers
{
	public class MultiMenuHandler: ContentHandler
	{
		public MultiMenuHandler(IRepository<MultiMenuPartRecord> repository)
		{
			Filters.Add(StorageFilter.For(repository));
		}
	}
}