using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.UI.Resources;

namespace Zveen.MultiMenu
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();
            manifest.DefineScript("ZveenSuperfish").SetUrl("superfish.js");
            manifest.DefineScript("ZveenSupersubs").SetUrl("supersubs.js");
        }
    }
}