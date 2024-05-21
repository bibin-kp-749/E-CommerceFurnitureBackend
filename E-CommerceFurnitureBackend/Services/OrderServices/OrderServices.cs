using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.JwtServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Razorpay.Api;

namespace E_CommerceFurnitureBackend.Services.OrderServices
{
    public class OrderServices: IOrderServices
    {
        private readonly IConfiguration _configuration;
        private readonly string key;
        private readonly string Secret;
        private readonly IJwtServices _jwtService;
        private readonly UserDbContext _userDbContext;
        public OrderServices(IConfiguration configuration,IJwtServices jwtServices,UserDbContext userDbContext)
        {
            this._configuration = configuration;
            this.key = _configuration["Razorpay:Key"];
            this.Secret = _configuration["Razorpay:Secret"];
            this._jwtService = jwtServices;
            this._userDbContext = userDbContext;
        }
        public async Task<MerchantOrder> GenerateOrder(PaymentDto payment)
        {
            try
            {
                Random random = new Random();
                string TransactionId = random.Next(0, 10000).ToString();
                RazorpayClient client = new RazorpayClient(key, Secret);
                Dictionary<string, object> option = new Dictionary<string, object> {
                    {"amount", payment.Amount*100},
                    { "currency", "INR" },
                    { "receipt", "order_rcptid" },
                    { "payment_capture", 0 }
                    };
                Razorpay.Api.Order order = client.Order.Create(option);
                var OrderId =order["id"];
                MerchantOrder Order = new MerchantOrder
                {
                    OrderId = order.Attributes["id"],
                    RazorPayKey = key,
                    Amount = (int)payment.Amount*100,
                    Currency = "INR",
                    Name = payment.Name,
                    Email = payment.Email,
                    PhoneNumber = payment.PhoneNumber,
                    Address = payment.Address,
                    Description = "Payment by Merchant"
                };
                return await Task.FromResult(Order);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public async Task<bool> CapturePayment(CapturePaymentDto Value)
        //{
        //    RazorpayClient client=new RazorpayClient(key,Secret);
        //    //Payment payment=Client.Payment.Fetch(Value.PaymentId);
        //    Dictionary<string, string> options = new Dictionary<string, string>
        //    {
        //        { "paymentId",Value.PaymentId.ToString()},
        //        {"orderId",Value.OrderId.ToString() },
        //    };
        //    Utils.verifyPaymentSignature(options);
        //    return true;
        //    //Payment PaymentCapture = payment.Capture(options);
        //    //string amt = PaymentCapture.Attributes["amount"];
        //    //return PaymentCapture.Attributes["status"];
        //}
        public async Task<string> CapturePayment(IHttpContextAccessor _httpContextAccessor)
        {
            try
            {
                string PaymentId = _httpContextAccessor.HttpContext.Request.Form["rzp_paymentid"];
                string OrderId = _httpContextAccessor.HttpContext.Request.Form["rzp_orderid"];
                RazorpayClient client = new RazorpayClient(key, Secret);
                Razorpay.Api.Payment payment = client.Payment.Fetch(PaymentId);
                Dictionary<string, object> option = new Dictionary<string, object>();
                option.Add("amount", payment.Attributes["amount"]);
                Payment paymentcapture = payment.Capture(option);
                string amt = paymentcapture.Attributes["amount"];
                return paymentcapture.Attributes["status"];
            }catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<OrderDto>> OrderDetails(string token)
        {
            try
            {
                var JwtToken =await _jwtService.GetUserIdFromToken(token);
                int UserId = Convert.ToInt32(JwtToken);
            if (UserId == null)
                return new List<OrderDto>();
            var data =await _userDbContext.Order
                    .Include(c => c.orderItems)
                    .ThenInclude(p=>p.Product).Where(c=>c.CustomerId==UserId).ToListAsync();
             var details=await OrderData(data);  
                if(details==null)
                    return new List<OrderDto>();
                return details;
            }catch (Exception ex)
            {
                throw new Exception($"Internal server{ex.Message}");
            }
        }
        public async Task<List<OrderDto>> OrderDetailsAdmin(int userId)
        {
            try
            {
                var data = await _userDbContext.Order
                    .Include(o => o.orderItems).ThenInclude(p => p.Product)
                    .Where(o => o.CustomerId == userId).ToListAsync();
                var details=await OrderData(data);
                if(details==null)
                    return new List<OrderDto>();
                return details;
            } catch (Exception ex)
            {
                throw new Exception($"Internal server error {ex.Message}");
            }
        }
        public async Task<List<OrderDto>> OrderData(List<Models.Order> data)
        {
            List<OrderDto> details = new List<OrderDto>();
            foreach (var item in data)
            {
                foreach (var orderitem in item.orderItems)
                {

                    OrderDto value = new OrderDto
                    {
                        CustomerCity = item.CustomerCity,
                        CustomerEmail = item.CustomerEmail,
                        CustomerName = item.CustomerName,
                        CustomerHomeAddress = item.CustomerHomeAddress,
                        CustomerPhoneNumber = item.CustomerPhoneNumber,
                        OrderId = item.OrderId,
                        OrderStatus = item.OrderStatus,
                        OrderTime = item.OrderTime,
                        ProductId = orderitem.ProductId,
                        Price = orderitem.Price,
                        Quantity = orderitem.Quantity,
                    };
                    details.Add(value);
                }
            }
            return details;
        }
    }
}
