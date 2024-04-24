namespace DoAnCoSo.Models
{
    public class tbChiTietNoiDung
    {
        public int Id {  get; set; }
        public int CuocThiId { get; set; }
        public tbCuocThi CuocThi { get; set; }
        public int NoiDungId { get; set; }
        public tbNoiDung NoiDung { get; set; }

    }
}
