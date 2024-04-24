using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnCoSo.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbPhieuDangKy_AspNetUsers_UserId1",
                table: "tbPhieuDangKy");

            migrationBuilder.DropIndex(
                name: "IX_tbPhieuDangKy_UserId1",
                table: "tbPhieuDangKy");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "tbPhieuDangKy");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "tbPhieuDangKy",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_tbPhieuDangKy_UserId",
                table: "tbPhieuDangKy",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbPhieuDangKy_AspNetUsers_UserId",
                table: "tbPhieuDangKy",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbPhieuDangKy_AspNetUsers_UserId",
                table: "tbPhieuDangKy");

            migrationBuilder.DropIndex(
                name: "IX_tbPhieuDangKy_UserId",
                table: "tbPhieuDangKy");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "tbPhieuDangKy",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
