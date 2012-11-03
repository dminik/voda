using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zveen.MultiMenu.Models;

namespace System.Web.Mvc
{
    public static class UrlHelperExtensioms
    {
        public static string MultiMenuAdminList(this UrlHelper helper, int idMultiMenu)
        {
            return helper.Action("List", "MultiMenuAdmin", new { area = "Zveen.MultiMenu", idMultiMenu = idMultiMenu });
        }
    }
}