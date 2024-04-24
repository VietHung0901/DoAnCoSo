using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        public ApplicationDbContext() 
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-BGSRQD70\\SQLEXPRESS;Initial Catalog=MOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }
        public DbSet<tbLoaiTruong> tbLoaiTruong { get; set; }
        public DbSet<tbTruong> tbTruong { get; set; }
        public DbSet<tbPhieuDangKy> tbPhieuDangKy { get; set; }
        public DbSet<tbCuocThi> tbCuocThi { get; set; }
        public DbSet<tbChiTietNoiDung> tbChiTietNoiDung { get; set; }
        public DbSet<tbNoiDung> tbNoiDung { get; set; }
        public DbSet<tbMonThi> tbMonThi { get; set; }
        public DbSet<tbChiTietQuyDinh> tbChiTietQuyDinh { get; set; }
        public DbSet<tbQuyDinh> tbQuyDinh { get; set; }
        public DbSet<tbPhieuKetQua> tbPhieuKetQua { get; set; }
    }
}
