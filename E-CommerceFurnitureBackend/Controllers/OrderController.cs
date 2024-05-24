using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.OrderServices;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("GenerateOrder")]
        [Authorize]
        public async Task<IActionResult> GenerateOrder(PaymentDto payment )
        {
            var response=await _orderServices.GenerateOrder(payment);
            return Ok(response);
        }
        [HttpPost("CapturePayment")]
        [Authorize]
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
        [HttpPost("OrderDetailsOfUser")]
        [Authorize]
        public async Task<IActionResult> OrderDetails()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                if (string.IsNullOrEmpty(jwtToken))
                    return BadRequest("Token is not valid");
                var response = await _orderServices.OrderDetails(jwtToken);
                if(response.Count== 0)
                    return Ok("Not conain data");
                return Ok(response);
            }catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPost("OrderDetailsByAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OrderDetailsAdmin(int userId)
        {
            try
            {
                if (userId < 1)
                    return BadRequest();
                var response = await _orderServices.OrderDetailsAdmin(userId);
                if (response.Count== 0)
                    return Ok("Not contain data");
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpGet("TotalProductsPurchased")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Totalproductspurchased()
        {
            try
            {
                var response = await _orderServices.Totalproductspurchased();
                if (response < 1)
                    return StatusCode(404);
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpGet("Totalrevenue")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Totalrevenuegenerated()
        {
            try
            {
                var response = await _orderServices.Totalrevenuegenerated();
                if (response < 1)
                    return StatusCode(404);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}
