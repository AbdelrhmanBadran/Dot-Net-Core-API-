using Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    public class OrderWithPaymentSpecifications : BaseSpecification<Order>
    {
        public OrderWithPaymentSpecifications(string PaymentIntentId) 
            : base(order => order.PaymentIntentId == PaymentIntentId)
        {
        }
    }
}
