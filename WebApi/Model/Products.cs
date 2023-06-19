namespace WebApi.Model
{
    public class Products
    {
        public int Id { get; set; }
        public string Code{ get; set; }
        public string Name { get; set; }
        public byte Type { get; set; }
        public decimal Price { get; set; }
    }
}
