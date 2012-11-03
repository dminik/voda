using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Core.Routable.Models;
using Orchard.Widgets.Models;

namespace Zveen.MultiMenu.Models
{
	public class MultiMenuPart : ContentPart<MultiMenuPartRecord>
	{
		[Required]
        public string Title
        {
            get { return this.As<WidgetPart>().Title; }
            set { this.As<WidgetPart>().Title = value; }
        }

        
        public string Includes
        {
            get { return Record.Includes; }
            set { Record.Includes = value; }
        }
        
        public string Script
        {
            get { return Record.Script; }
            set { Record.Script = value; }
        }
        
        public string Style
        {
            get { return Record.Style; }
            set { Record.Style = value; }
        }
        
        public string BeforeHtml
        {
            get { return Record.BeforeHtml; }
            set { Record.BeforeHtml = value; }
        }
        
        public string ElementHtml
        {
            get { return Record.ElementHtml; }
            set { Record.ElementHtml = value; }
        }
		
		public string ElementHtmlLevel2
        {
            get { return Record.ElementHtmlLevel2; }
            set { Record.ElementHtmlLevel2 = value; }
        }
		
		public string ElementHtmlLevel3
        {
            get { return Record.ElementHtmlLevel3; }
            set { Record.ElementHtmlLevel3 = value; }
        }
        
        public string AfterHtml
        {
            get { return Record.AfterHtml; }
            set { Record.AfterHtml = value; }
        }
	}
}