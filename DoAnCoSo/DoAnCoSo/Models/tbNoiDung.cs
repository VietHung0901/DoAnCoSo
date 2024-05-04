namespace DoAnCoSo.Models
{
    public class tbNoiDung
    {
        public int Id { get; set; }
        public string NoiDungThi { get; set; }
        public string TenNoiDung { get; set; }
        public string? imageUrl { get; set; }
        public byte? TrangThai { get; set; }

        public List<tbChiTietNoiDung> ChiTietNoiDungs { get; set; }
    }
}
