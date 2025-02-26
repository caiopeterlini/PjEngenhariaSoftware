using Model.Entities;

namespace Model.Interfaces
{
    public interface IOrderProductRepository
    {
        public void Insert(OrderProduct orderProduct);
    }
}
