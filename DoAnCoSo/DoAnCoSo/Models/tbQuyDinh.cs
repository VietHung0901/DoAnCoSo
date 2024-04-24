namespace DoAnCoSo.Models
{
    public class tbQuyDinh
    {
        public int Id { get; set; }
        public string TenQuyDinh { get; set; }
        public string NoiDungQuyDinh { get; set; }

        public List<tbChiTietQuyDinh> ChiTietQuyDinhs { get; set; }
    }
}
