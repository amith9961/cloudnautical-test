using CloudNautical.DataAccess.ADO.Repository;
using CloudNautical.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudNautical.Domain.Services
{
    public class DeliveryService
    {
        private readonly OrderRepository _customerRepo;

        public DeliveryService(OrderRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public async Task<OrderDetails> GetDeliveryDetails(string customerId, string userId)
        {
            return await _customerRepo.GetDeliveryDetails(customerId, userId);
        }
    }
}
