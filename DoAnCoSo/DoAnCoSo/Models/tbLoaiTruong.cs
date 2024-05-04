using System.ComponentModel.DataAnnotations;

namespace DoAnCoSo.Models
{
    public class tbLoaiTruong
    {
        public int Id { get; set; }
        public string TenLoaiTruong { get; set; }
        public byte? TrangThai { get; set; }

        public List<tbTruong> Truongs { get; set; }
    }
}
