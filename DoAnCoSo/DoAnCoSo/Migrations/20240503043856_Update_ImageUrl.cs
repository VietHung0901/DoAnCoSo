using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnCoSo.Migrations
{
    /// <inheritdoc />
    public partial class Update_ImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "tbNoiDung",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "tbNoiDung");
        }
    }
}
