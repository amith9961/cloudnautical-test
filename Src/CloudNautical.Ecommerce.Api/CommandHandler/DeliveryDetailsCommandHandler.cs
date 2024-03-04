using CloudNautical.Domain.Entities.Order;
using CloudNautical.Domain.Services;
using CloudNautical.Ecommerce.Api.Command;
using MediatR;

namespace CloudNautical.Ecommerce.Api.CommandHandler
{
    public class DeliveryDetailsCommandHandler : IRequestHandler<DeliveryDetailsCommand, DeliveryDetailsResponse>
    {
        private readonly DeliveryService _deliveryService;

        public DeliveryDetailsCommandHandler(DeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public async Task<DeliveryDetailsResponse> Handle(DeliveryDetailsCommand request, CancellationToken cancellationToken)
        {
            var deliverytInfo = await _deliveryService.GetDeliveryDetails(request.CustomerId, request.User);
            if (deliverytInfo is null || deliverytInfo.OrderId is 0)
            {
                return new DeliveryDetailsResponse();
            }
            if(request.User != deliverytInfo.Email)
            {
                throw new ArgumentException("Invalid data provided.", nameof(request.User));
            }
            return MapToDeliveryDetailsResponse(deliverytInfo);

        }
        #region PRIVATE
        private static DeliveryDetailsResponse MapToDeliveryDetailsResponse(OrderDetails orderDetails)
        {
            return new DeliveryDetailsResponse
            {
                Customer = new Customer
                {
                    FirstName = orderDetails.FirstName,
                    LastName = orderDetails.LastName,
                },
                Order = new Order
                {
                    OrderNumber = orderDetails.OrderId,
                    OrderDate = orderDetails.OrderDate,
                    DeliveryExpected = orderDetails.DeliveryExpected,
                    OrderItems = mapToOderItems(orderDetails.OrderItems,orderDetails.ContainsGift),
                    DeliveryAddress = CreateDeliveryAddress(orderDetails)
                }
            };
        }

        private static string CreateDeliveryAddress(OrderDetails orderDetails)
        {
            return $"{orderDetails.HouseNo} {orderDetails.Street} {orderDetails.Town} {orderDetails.Postcode}";
        }

        private static List<Command.OrderItem> mapToOderItems(List<Domain.Entities.Order.OrderItem> orderItems, int containsGift)
        {
            var items = new List<Command.OrderItem>();
            foreach (var orderItem in orderItems)
            {
                items.Add(new Command.OrderItem
                {
                    PriceEach = orderItem.Price,
                    Product = containsGift == 1 ? "Gift":orderItem.ProductName,
                    Quantity = orderItem.Quantity,
                });
            }
            return items;
        }
        #endregion
    }
}
