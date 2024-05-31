namespace DoAnCoSo.Models
{
    public class tbMonThi
    {
        public int Id { get; set; }
        public string TenMonThi { get; set; }
        public byte? TrangThai { get; set; }
        public List<tbCuocThi> CuocThi { get; set;}
    }
}
