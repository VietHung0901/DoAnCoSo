using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnCoSo.Migrations
{
    /// <inheritdoc />
    public partial class Initail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbLoaiTruong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiTruong = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbLoaiTruong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbMonThi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMonThi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbMonThi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbNoiDung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDungThi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenNoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbNoiDung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbQuyDinh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuyDinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDungQuyDinh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbQuyDinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbTruong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTruong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiTruongId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbTruong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbTruong_tbLoaiTruong_LoaiTruongId",
                        column: x => x.LoaiTruongId,
                        principalTable: "tbLoaiTruong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbCuocThi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayThi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuongThiSinh = table.Column<int>(type: "int", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonThiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCuocThi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbCuocThi_tbMonThi_MonThiId",
                        column: x => x.MonThiId,
                        principalTable: "tbMonThi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbChiTietNoiDung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuocThiId = table.Column<int>(type: "int", nullable: false),
                    NoiDungId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbChiTietNoiDung", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbChiTietNoiDung_tbCuocThi_CuocThiId",
                        column: x => x.CuocThiId,
                        principalTable: "tbCuocThi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbChiTietNoiDung_tbNoiDung_NoiDungId",
                        column: x => x.NoiDungId,
                        principalTable: "tbNoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbChiTietQuyDinh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuocThiId = table.Column<int>(type: "int", nullable: false),
                    QuyDinhId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbChiTietQuyDinh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbChiTietQuyDinh_tbCuocThi_CuocThiId",
                        column: x => x.CuocThiId,
                        principalTable: "tbCuocThi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbChiTietQuyDinh_tbQuyDinh_QuyDinhId",
                        column: x => x.QuyDinhId,
                        principalTable: "tbQuyDinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbPhieuDangKy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayDangKy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CuocThiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbPhieuDangKy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbPhieuDangKy_tbCuocThi_CuocThiId",
                        column: x => x.CuocThiId,
                        principalTable: "tbCuocThi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbPhieuKetQua",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhieuDangKyId = table.Column<int>(type: "int", nullable: false),
                    Phut = table.Column<int>(type: "int", nullable: false),
                    Giay = table.Column<int>(type: "int", nullable: false),
                    Diem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbPhieuKetQua", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbPhieuKetQua_tbPhieuDangKy_PhieuDangKyId",
                        column: x => x.PhieuDangKyId,
                        principalTable: "tbPhieuDangKy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbChiTietNoiDung_CuocThiId",
                table: "tbChiTietNoiDung",
                column: "CuocThiId");

            migrationBuilder.CreateIndex(
                name: "IX_tbChiTietNoiDung_NoiDungId",
                table: "tbChiTietNoiDung",
                column: "NoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_tbChiTietQuyDinh_CuocThiId",
                table: "tbChiTietQuyDinh",
                column: "CuocThiId");

            migrationBuilder.CreateIndex(
                name: "IX_tbChiTietQuyDinh_QuyDinhId",
                table: "tbChiTietQuyDinh",
                column: "QuyDinhId");

            migrationBuilder.CreateIndex(
                name: "IX_tbCuocThi_MonThiId",
                table: "tbCuocThi",
                column: "MonThiId");

            migrationBuilder.CreateIndex(
                name: "IX_tbPhieuDangKy_CuocThiId",
                table: "tbPhieuDangKy",
                column: "CuocThiId");

            migrationBuilder.CreateIndex(
                name: "IX_tbPhieuKetQua_PhieuDangKyId",
                table: "tbPhieuKetQua",
                column: "PhieuDangKyId");

            migrationBuilder.CreateIndex(
                name: "IX_tbTruong_LoaiTruongId",
                table: "tbTruong",
                column: "LoaiTruongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbChiTietNoiDung");

            migrationBuilder.DropTable(
                name: "tbChiTietQuyDinh");

            migrationBuilder.DropTable(
                name: "tbPhieuKetQua");

            migrationBuilder.DropTable(
                name: "tbTruong");

            migrationBuilder.DropTable(
                name: "tbNoiDung");

            migrationBuilder.DropTable(
                name: "tbQuyDinh");

            migrationBuilder.DropTable(
                name: "tbPhieuDangKy");

            migrationBuilder.DropTable(
                name: "tbLoaiTruong");

            migrationBuilder.DropTable(
                name: "tbCuocThi");

            migrationBuilder.DropTable(
                name: "tbMonThi");
        }
    }
}
