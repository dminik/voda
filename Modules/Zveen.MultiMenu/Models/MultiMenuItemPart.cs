using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Core.Routable.Models;
using Orchard.ContentManagement.Aspects;

namespace Zveen.MultiMenu.Models
{
	public class MultiMenuItemPart : ContentPart<MultiMenuItemPartRecord>
	{
        [Required]
        public MultiMenuPart MultiMenuPart
        {
            get { return this.As<ICommonPart>().Container.As<MultiMenuPart>(); }
            set { this.As<ICommonPart>().Container = value; }
        }

		[Required]
        public string Title
        {
            get { return Record.Title; }
            set { Record.Title = value; }
        }

		[Required]
		public String Url
		{
            get { return Record.Url; }
            set { Record.Url = value; }
		}
        [Required]
        public string Position
        {
            get { return Record.Position; }
            set { Record.Position = value; }
        }


	}
}