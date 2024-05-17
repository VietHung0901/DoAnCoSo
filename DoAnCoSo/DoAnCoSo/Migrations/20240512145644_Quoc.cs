using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnCoSo.Migrations
{
    /// <inheritdoc />
    public partial class Quoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbLoaiTruong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiTruong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: true)
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
                    TenMonThi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: true)
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
                    TenNoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: true)
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
                    TenQuyDinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDungQuyDinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbQuyDinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbTruong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTruong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiTruongId = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: true)
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
                    TenCuocThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuongThiSinh = table.Column<int>(type: "int", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: true),
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<int>(type: "int", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruongId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_tbTruong_TruongId",
                        column: x => x.TruongId,
                        principalTable: "tbTruong",
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                    CuocThiId = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbPhieuDangKy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbPhieuDangKy_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Diem = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: true)
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TruongId",
                table: "AspNetUsers",
                column: "TruongId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_tbPhieuDangKy_UserId",
                table: "tbPhieuDangKy",
                column: "UserId");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "tbChiTietNoiDung");

            migrationBuilder.DropTable(
                name: "tbChiTietQuyDinh");

            migrationBuilder.DropTable(
                name: "tbPhieuKetQua");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "tbNoiDung");

            migrationBuilder.DropTable(
                name: "tbQuyDinh");

            migrationBuilder.DropTable(
                name: "tbPhieuDangKy");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tbCuocThi");

            migrationBuilder.DropTable(
                name: "tbTruong");

            migrationBuilder.DropTable(
                name: "tbMonThi");

            migrationBuilder.DropTable(
                name: "tbLoaiTruong");
        }
    }
}
