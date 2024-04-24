namespace DoAnCoSo.Models
{
    public class tbCuocThi
    {
        public int Id { get; set; }
        public DateTime NgayThi { get; set; }
        public int SoLuongThiSinh { get; set; }
        public string DiaDiem { get; set; }

        //Nối Khóa
        public int MonThiId { get; set; }
        public tbMonThi MonThi { get; set; }

        public List<tbPhieuDangKy> PhieuDangKys { get; set; }
        public List<tbChiTietQuyDinh> QuyDinhs { get; set; }
        public List<tbChiTietNoiDung> NoiDungs { get; set; }

    }
}
