using MediatR;

namespace CloudNautical.Ecommerce.Api.Command
{
    public class DeliveryDetailsCommand:IRequest<DeliveryDetailsResponse>
    {
        public string User { get; set; }
        public string CustomerId { get; set; }
    }
}
