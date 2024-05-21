using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.OrderServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace E_CommerceFurnitureBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private IHttpContextAccessor _contextAccessor;
        public OrderController(IOrderServices orderServices,IHttpContextAccessor httpContextAccessor)
        {
            this._orderServices = orderServices;
            this._contextAccessor = httpContextAccessor;
        }
        [HttpPost]
        public async Task<IActionResult> GenerateOrder(PaymentDto payment )
        {
            var response=await _orderServices.GenerateOrder(payment);
            return Ok(response);
        }
        [HttpPost("hi")]
        public async Task<IActionResult> CapturePayment()
        {
            try
            {
                // Set the Content-Type header
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");

                // Make your HTTP request to the CapturePayment endpoint here

                // Since this action is in the CartController, you should implement the logic related to cart functionality here
                // For now, let's just return a dummy response
                return Ok("Payment captured successfully!");
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("OrderDetails")]
        public async Task<IActionResult> OrderDetails(string token)
        {
            try
            {
                if (token.Length < 1)
                    return BadRequest();
                var response = await _orderServices.OrderDetails(token);
                return Ok(response);
            }catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }

}
