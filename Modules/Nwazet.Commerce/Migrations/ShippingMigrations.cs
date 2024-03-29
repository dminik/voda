﻿using System.Data;
using Orchard.ContentManagement.MetaData;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace Nwazet.Commerce.Migrations {
    [OrchardFeature("Nwazet.Shipping")]
    public class ShippingMigrations : DataMigrationImpl {

        public int Create() {
            SchemaBuilder.CreateTable("WeightBasedShippingMethodPartRecord", table => table
                .ContentPartRecord()
                .Column("Name", DbType.String)
                .Column("ShippingCompany", DbType.String)
                .Column("Price", DbType.Double)
                .Column("MinimumWeight", DbType.Double, column => column.Nullable())
                .Column("MaximumWeight", DbType.Double, column => column.Nullable())
                .Column("IncludedShippingAreas", DbType.String)
                .Column("ExcludedShippingAreas", DbType.String)
            );

            ContentDefinitionManager.AlterTypeDefinition("WeightBasedShippingMethod", cfg => cfg
              .WithPart("WeightBasedShippingMethodPart")
              .WithPart("TitlePart"));

            return 1;
        }
    }
}
