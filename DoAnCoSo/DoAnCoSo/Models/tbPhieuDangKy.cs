namespace DoAnCoSo.Models
{
    public class tbPhieuDangKy
    {
        public int Id { get; set; }
        public DateTime NgayDangKy { get; set; }
        public int CuocThiId { get; set; }
        public byte? TrangThai { get; set; }
        public tbCuocThi CuocThi { get; set; }
        public List<tbPhieuKetQua> PhieuKetQuas { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
