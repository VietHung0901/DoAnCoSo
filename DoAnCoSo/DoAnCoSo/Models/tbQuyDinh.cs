namespace DoAnCoSo.Models
{
    public class tbQuyDinh
    {
        public int Id { get; set; }
        public string? TenQuyDinh { get; set; }
        public string? NoiDungQuyDinh { get; set; }
        public string? imageURL { get; set; }
        public byte? TrangThai { get; set; }
        public List<tbChiTietQuyDinh>? ChiTietQuyDinhs { get; set; }
    }
}
