using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbDrivenFM.App.Migrations
{
    /// <inheritdoc />
    public partial class DbFeatureFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeatureFlags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureFlags", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FeatureFlags",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Weather" });

            migrationBuilder.InsertData(
                table: "FeatureFlags",
                columns: new[] { "Id", "Enabled", "Name" },
                values: new object[] { 2, true, "Counter" });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureFlags_Name",
                table: "FeatureFlags",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureFlags");
        }
    }
}
