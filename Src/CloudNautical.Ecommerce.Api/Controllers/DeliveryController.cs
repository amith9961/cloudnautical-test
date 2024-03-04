using CloudNautical.Ecommerce.Api.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudNautical.Ecommerce.Api.Controllers
{

    [ApiController]
    [Route("api/delivery/")]
    public class DeliveryController : ApiControllerBase
    {
        public DeliveryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("details")]
        public async Task<IActionResult> DeliveryDetails([FromBody] DeliveryDetailsCommand request)
        {
            try
            {
               return Ok(await base.HandleAsync(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
