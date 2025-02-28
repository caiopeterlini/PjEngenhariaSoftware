using QueueMessage.Consumer.Models;
using System.Text.Json.Serialization;

namespace QueueMessage.Consumer.Request
{
    public class RequestOrder
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ItemOrdem>? ItensP { get; set; }
    }

    public class ItemOrdem
    {
        public Produto Produto { get; set; } = new Produto();
        public int Quantity
        {
            get; set;
        }
    }
    public class Produto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public object? Itens { get; set; } 
    }
}
