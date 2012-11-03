using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zveen.MultiMenu.Models;

namespace Zveen.MultiMenu.ViewModels
{
        public class MultiMenuItemViewModel
        {
            public Int32 Id { get; set; }
            public String Title { get; set; }
            public String Position { get; set; }
            public String Url { get; set; }

            public MultiMenuItemViewModel(MultiMenuItemPart item)
            {
                Id = item.Id;
                Title = item.Title;
                Position = item.Position;
                Url = item.Url;
            }

            public MultiMenuItemViewModel()
            {
            }
        }

        public class MultiMenuViewModel
        {
            public MultiMenuViewModel()
            {
                MenuItemEntries = new List<MultiMenuItemViewModel>();
            }

            public MultiMenuPart MultiMenu { get; set; }
            public MultiMenuItemPart NewMenuItem { get; set; }
            public IList<MultiMenuItemViewModel> MenuItemEntries { get; set; }
        }
}