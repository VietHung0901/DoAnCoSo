namespace DoAnCoSo.Models
{
    public class tbPhieuKetQua
    {
        public int Id { get; set; }
        public int PhieuDangKyId { get; set; }
        public tbPhieuDangKy PhieuDangKy { get; set; }
        public int Phut {  get; set; }
        public int Giay { get; set; }
        public int Diem { get; set; }

    }
}
