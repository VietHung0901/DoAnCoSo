using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnCoSo.Migrations
{
    /// <inheritdoc />
    public partial class update_tbPhieuDangKy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "tbPhieuDangKy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoDienThoai",
                table: "tbPhieuDangKy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TruongId",
                table: "tbPhieuDangKy",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "tbPhieuDangKy");

            migrationBuilder.DropColumn(
                name: "SoDienThoai",
                table: "tbPhieuDangKy");

            migrationBuilder.DropColumn(
                name: "TruongId",
                table: "tbPhieuDangKy");
        }
    }
}
