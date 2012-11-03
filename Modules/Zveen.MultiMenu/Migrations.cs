using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Zveen.MultiMenu {
    public class Migrations : DataMigrationImpl {

        public int Create() {

			SchemaBuilder.CreateTable("MultiMenuPartRecord", table => table
                .ContentPartRecord()
				.Column<string>("Includes", column => column.Unlimited())
				.Column<string>("Script", column => column.Unlimited())
				.Column<string>("Style", column => column.Unlimited())
				.Column<string>("BeforeHtml", column => column.Unlimited())
				.Column<string>("ElementHtml", column => column.Unlimited())
				.Column<string>("ElementHtmlLevel2", column => column.Unlimited())
				.Column<string>("ElementHtmlLevel3", column => column.Unlimited())
				.Column<string>("AfterHtml", column => column.Unlimited())
            );
			SchemaBuilder.CreateTable("MultiMenuItemPartRecord", table => table
				.ContentPartRecord()
				.Column("Position", DbType.String)
				.Column("Url", DbType.String)
				.Column("Title", DbType.String)
			);

            ContentDefinitionManager.AlterTypeDefinition("ZveenMultiMenu",
            cfg => cfg
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithPart("MultiMenuPart")
                .WithSetting("Stereotype", "Widget"));

            ContentDefinitionManager.AlterTypeDefinition("ZveenMultiMenuItem",
            cfg => cfg
                .WithPart("MultiMenuItem")
                .WithPart("CommonPart")
                .WithPart("MultiMenuItemPart"));

            return 1;
        }
    }
}