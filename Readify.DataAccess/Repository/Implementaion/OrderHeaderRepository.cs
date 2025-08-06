using Readify.DataAccess.Data;
using Readify.DataAccess.Repository.Interfaces;
using Readify.Models;
using Readify.Utility.enums;

namespace Readify.DataAccess.Repository.Implementaion
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDBContext _dbContext;
        public OrderHeaderRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
        public void UpdateStatus(int id, OrderStatus orderStatus, PaymentStatus paymentStatus = PaymentStatus.None)
        {
            var orderFromDb = _dbContext.OrderHeaders.FirstOrDefault(o => o.Id == id);
            if (orderFromDb is not null)
            {
                orderFromDb.OrderStatus = orderStatus.ToString();
                if (paymentStatus is not PaymentStatus.None)
                {
                    orderFromDb.PaymentStatus = paymentStatus.ToString();
                }
            }
        }

        public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
        {
            var orderFromDb = _dbContext.OrderHeaders.FirstOrDefault(o => o.Id == id);
            if (!string.IsNullOrEmpty(sessionId))
            {
                orderFromDb.SessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                orderFromDb.PaymentIntentId = paymentIntentId;
                orderFromDb.PaymentDate = DateTime.Now;
            }
        }


        public void Update(OrderHeader orderHeader)
        {
            _dbContext.Set<OrderHeader>().Update(orderHeader);
        }
    }
}
