using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.DisplayManagement.Descriptors;
using Orchard;
using Orchard.ContentManagement;
using System.Diagnostics;
using Orchard.Widgets.Models;

namespace Zveen.MultiMenu.Alternates
{
    public class ZveenMultiMenuAlternates : IShapeTableProvider
    {
        private readonly IWorkContextAccessor _workContextAccessor;

        public ZveenMultiMenuAlternates(IWorkContextAccessor workContextAccessor)
        {
            _workContextAccessor = workContextAccessor;
        }

public void Discover(ShapeTableBuilder builder)
{
    //remove widget wrappers containing this Part
    builder.Describe("Widget")
    .OnDisplaying(displaying =>
    {
        if (displaying.Shape == null || displaying.Shape.ContentItem == null || displaying.Shape.ContentItem.ContentType != "ZveenMultiMenu")
            return;
        displaying.ShapeMetadata.Wrappers.Clear();
    });

    //adding alternate for our part
    builder.Describe("ZveenMultiMenu")
        .OnDisplaying(displaying =>
        {
            ContentItem ci = displaying.Shape.ContentItem;

            displaying.ShapeMetadata.Alternates.Add("ZveenMultiMenu_" + ci.As<WidgetPart>().Title);
            displaying.ShapeMetadata.Alternates.Add("ZveenMultiMenu_" + ci.As<WidgetPart>().Zone);
            displaying.ShapeMetadata.Alternates.Add("ZveenMultiMenu_" + ci.As<WidgetPart>().Zone + "_" + ci.As<WidgetPart>().Title);
        });
}
    }

}