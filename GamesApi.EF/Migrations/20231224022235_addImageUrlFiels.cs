using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesApi.EF.Migrations
{
    /// <inheritdoc />
    public partial class addImageUrlFiels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "Games");
        }
    }
}
