using System.Linq.Expressions;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderByPaymentIntendIdSpecification : BaseSpecifications<Order>
    {
        public OrderByPaymentIntendIdSpecification(string paymentInterdId)
         : base(o => o.PaymentIntendId == paymentInterdId)
        {
        }
    }
}