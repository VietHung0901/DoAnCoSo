using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnCoSo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "tbPhieuDangKy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "tbPhieuDangKy",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbPhieuDangKy_UserId1",
                table: "tbPhieuDangKy",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_tbPhieuDangKy_AspNetUsers_UserId1",
                table: "tbPhieuDangKy",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbPhieuDangKy_AspNetUsers_UserId1",
                table: "tbPhieuDangKy");

            migrationBuilder.DropIndex(
                name: "IX_tbPhieuDangKy_UserId1",
                table: "tbPhieuDangKy");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tbPhieuDangKy");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "tbPhieuDangKy");
        }
    }
}
