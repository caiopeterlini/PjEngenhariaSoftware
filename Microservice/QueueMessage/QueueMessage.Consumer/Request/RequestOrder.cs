using System.Text.Json.Serialization;

namespace QueueMessage.Consumer.Request
{
    public class RequestOrder
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<Item>? Itens { get; set; }
    }

    public class Item
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
