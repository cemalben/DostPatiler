using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DostPatiler.Migrations
{
    /// <inheritdoc />
    public partial class fiftjh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "surec",
                table: "Hayvanlar",
                newName: "Durum");

            migrationBuilder.AddColumn<bool>(
                name: "Sahiplendirme",
                table: "Hayvanlar",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sahiplendirme",
                table: "Hayvanlar");

            migrationBuilder.RenameColumn(
                name: "Durum",
                table: "Hayvanlar",
                newName: "surec");
        }
    }
}
