using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnCoSo.Migrations
{
    /// <inheritdoc />
    public partial class AddTrangThai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "TrangThai",
                table: "tbTruong",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "TrangThai",
                table: "tbQuyDinh",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "TrangThai",
                table: "tbPhieuKetQua",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "TrangThai",
                table: "tbPhieuDangKy",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "TrangThai",
                table: "tbNoiDung",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "tbNoiDung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "TrangThai",
                table: "tbMonThi",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "TrangThai",
                table: "tbLoaiTruong",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenCuocThi",
                table: "tbCuocThi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "TrangThai",
                table: "tbCuocThi",
                type: "tinyint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "tbTruong");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "tbQuyDinh");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "tbPhieuKetQua");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "tbPhieuDangKy");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "tbNoiDung");

            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "tbNoiDung");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "tbMonThi");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "tbLoaiTruong");

            migrationBuilder.DropColumn(
                name: "TenCuocThi",
                table: "tbCuocThi");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "tbCuocThi");
        }
    }
}
