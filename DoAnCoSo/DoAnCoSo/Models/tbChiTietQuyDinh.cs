namespace DoAnCoSo.Models
{
    public class tbChiTietQuyDinh
    {
        public int Id { get; set; }
        public int CuocThiId { get; set;}
        public tbCuocThi CuocThi { get; set;}
        public int QuyDinhId { get; set;}
        public tbQuyDinh QuyDinh { get; set;}
    }
}
