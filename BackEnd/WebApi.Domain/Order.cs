namespace WebApi.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public double Price { get; set; }
        public List<ItensOrder>? Itens { get; set; }
    }
}
