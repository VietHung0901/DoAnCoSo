namespace DoAnCoSo.Models
{
    public class tbCuocThi
    {
        public int Id { get; set; }
        public string? TenCuocThi { get; set; }
        public DateTime NgayThi { get; set; }
        public int SoLuongThiSinh { get; set; }
        public string DiaDiem { get; set; }
        public byte? TrangThai { get; set; }
        public int? LoaiTruongId { get; set; }
        //Nối Khóa
        public int MonThiId { get; set; }
        public tbMonThi MonThi { get; set; }

        public List<tbPhieuDangKy> PhieuDangKys { get; set; }
        public List<tbChiTietQuyDinh> QuyDinhs { get; set; }
        public List<tbChiTietNoiDung> NoiDungs { get; set; }

    }
}
