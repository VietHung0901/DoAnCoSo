namespace DoAnCoSo.Models
{
    public class tbTruong
    {
        public int Id { get; set; }
        public string TenTruong { get; set; }
        public int LoaiTruongId { get;set; }
        public byte? TrangThai { get; set; }

        public tbLoaiTruong LoaiTruong { get; set; }
    }
}
