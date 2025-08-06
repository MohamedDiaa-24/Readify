using Readify.DataAccess.Repository.Interfaces.IRepository;
using Readify.Models;
using Readify.Utility.enums;

namespace Readify.DataAccess.Repository.Interfaces
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);

        void UpdateStatus(int id, OrderStatus orderStatus, PaymentStatus paymentStatus = PaymentStatus.None);
        void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId);
    }
}
