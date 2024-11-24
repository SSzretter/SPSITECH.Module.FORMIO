using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace SPSITECH.Module.FORMIO.Migrations.EntityBuilders
{
    public class FORMIOEntityBuilder : AuditableBaseEntityBuilder<FORMIOEntityBuilder>
    {
        private const string _entityTableName = "SPSITECHFORMIO";
        private readonly PrimaryKey<FORMIOEntityBuilder> _primaryKey = new("PK_SPSITECHFORMIO", x => x.FORMIOId);
        private readonly ForeignKey<FORMIOEntityBuilder> _moduleForeignKey = new("FK_SPSITECHFORMIO_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public FORMIOEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override FORMIOEntityBuilder BuildTable(ColumnsBuilder table)
        {
            FORMIOId = AddAutoIncrementColumn(table,"FORMIOId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> FORMIOId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
