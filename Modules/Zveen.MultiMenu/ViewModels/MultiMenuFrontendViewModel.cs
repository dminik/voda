using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zveen.MultiMenu.Services;
using Zveen.MultiMenu.Models;
using System.Web.Mvc;
using System.Text;
using System.Diagnostics;

namespace Zveen.MultiMenu.ViewModels
{
    public class MultiMenuFrontendViewModel
    {
        public class MultiMenuItemFrontendViewModel
        {
            public String Url { get; set; }
            public String Title { get; set; }
            public String[] Positions { get; set; }
            public IList<MultiMenuItemFrontendViewModel> SubMenus { get; set; }

            public MultiMenuItemFrontendViewModel()
            {
                SubMenus = new List<MultiMenuItemFrontendViewModel>();
            }
            
            public MultiMenuItemFrontendViewModel(MultiMenuItemPart part)
                :this()
            {
                Url = part.Url;
                Title = part.Title;
                Positions = part.Position.Split('.');
            }
        }

        public Boolean DisplayTitle { get; set; }
        //public Boolean DisplayRootOnly { get; set; }

        public String Title { get; set; }
        public String Include { get; set; }
        public String Script { get; set; }

        public String BeforeMenu { get; set; }
        public String AfterMenu { get; set; }
        public String ElementHtml { get; set; }
		public String ElementHtmlLevel2 { get; set; }
		public String ElementHtmlLevel3 { get; set; }

        public IList<MultiMenuItemFrontendViewModel> MenuItems { get; set; }

        public MultiMenuFrontendViewModel(Models.MultiMenuPart part, IEnumerable<MultiMenuItemPart> menuItemParts)
        {
            Title = part.Title;
            String Id = part.Title.Replace(' ','_').Replace('-','_');

			if(String.IsNullOrEmpty(part.BeforeHtml))
				BeforeMenu = "";
			else 
				BeforeMenu = part.BeforeHtml.Replace("{Id}", Id);
				
			ElementHtml = part.ElementHtml;
			
			if(String.IsNullOrEmpty( part.ElementHtmlLevel2))
				ElementHtmlLevel2 = ElementHtml;
			else
				ElementHtmlLevel2 = part.ElementHtmlLevel2;
				
			if(String.IsNullOrEmpty( part.ElementHtmlLevel3))
				ElementHtmlLevel3 = ElementHtmlLevel2;
			else
				ElementHtmlLevel3 = part.ElementHtmlLevel3;	
				
		    AfterMenu = part.AfterHtml;

			if( String.IsNullOrEmpty(part.Includes) )
				Include = "";
			else
				Include = part.Includes
					.Replace("{superfish.js}", "<script src=\"/Modules/Zveen.MultiMenu/scripts/superfish.js\" type=\"text/javascript\"></script>")
					.Replace("{supersubs.js}", "<script src=\"/Modules/Zveen.MultiMenu/scripts/supersubs.js\" type=\"text/javascript\"></script>")
					.Replace("{hoverIntent.js}", "<script src=\"/Modules/Zveen.MultiMenu/scripts/hoverIntent.js\" type=\"text/javascript\"></script>")
					.Replace("{superfish.css}", "<link rel=\"stylesheet\" media=\"screen\" href=\"/ZveenMenu/superfishCss/" + part.Id + "\" />")
					.Replace("{superfish-vertical.css}", "<link rel=\"stylesheet\" media=\"screen\" href=\"/ZveenMenu/superfishVerticalCss/" + part.Id + "\" />")
					+ "<link rel=\"stylesheet\" media=\"screen\" href=\"/ZveenMenu/style/" + part.Id + "\" />";
				
			if(String.IsNullOrEmpty(part.Script))
				Script = "";
			else
				Script = part.Script.Replace("{Id}", Id);

            List<MultiMenuItemFrontendViewModel> items = new List<MultiMenuItemFrontendViewModel>();
            foreach(var menuItemPart in menuItemParts)
                items.Add(new MultiMenuItemFrontendViewModel(menuItemPart));
            
            List<MultiMenuItemFrontendViewModel> result = new List<MultiMenuItemFrontendViewModel>();

            var currentPerLevel = new Dictionary<int,MultiMenuItemFrontendViewModel>();
            currentPerLevel.Add(0, new MultiMenuItemFrontendViewModel());
            foreach( var item in items)
            {
                int level = item.Positions.Length;
                currentPerLevel[level-1].SubMenus.Add(item);
                currentPerLevel[level] = item;
            }

            MenuItems = new List<MultiMenuItemFrontendViewModel>();
            foreach (var item in currentPerLevel[0].SubMenus)
                MenuItems.Add(item);
        }
    

        public MvcHtmlString RenderMenu(MultiMenuFrontendViewModel menu, IList<MultiMenuItemFrontendViewModel> items, int level)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var item in items)
            {
				switch(level)
				{
					case 1:
						sb.AppendFormat( "<li>"+ menu.ElementHtml.Replace("{Url}",item.Url).Replace("{Title}",item.Title));
						break;
					case 2:
						sb.AppendFormat( "<li>"+ menu.ElementHtmlLevel2.Replace("{Url}",item.Url).Replace("{Title}",item.Title));
						break;
					case 3:
						sb.AppendFormat( "<li>"+ menu.ElementHtmlLevel3.Replace("{Url}",item.Url).Replace("{Title}",item.Title));
						break;
                }
				if (item.SubMenus.Count == 0)
                {
                    sb.AppendLine("</li>");
                    continue;
                }
                sb.AppendLine("<ul>");
                sb.Append(RenderMenu(menu, item.SubMenus,level+1));
                sb.AppendLine("</ul>");
                sb.AppendLine("</li>");
            }
            return new MvcHtmlString(sb.ToString()); 
        }
             
    }
}