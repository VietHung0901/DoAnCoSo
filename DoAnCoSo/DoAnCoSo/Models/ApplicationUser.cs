using Microsoft.AspNetCore.Identity;

namespace DoAnCoSo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? CCCD { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public int GioiTinh { get; set; }
        public string SoDienThoai { get; set; }
        public string ImageUrl { get; set; }
        public int TruongId { get; set; }
        public tbTruong Truong { get; set; }
    }
}
