using Readify.DataAccess.Repository.Interfaces.IRepository;
using Readify.Models;

namespace Readify.DataAccess.Repository.Interfaces
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail orderDetail);
    }
}
