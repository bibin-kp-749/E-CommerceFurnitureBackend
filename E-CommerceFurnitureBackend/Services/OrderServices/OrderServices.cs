using E_CommerceFurnitureBackend.Models.DTO;
using Microsoft.Extensions.Options;
using Razorpay.Api;

namespace E_CommerceFurnitureBackend.Services.OrderServices
{
    public class OrderServices: IOrderServices
    {
        private readonly IConfiguration _configuration;
        private readonly string key;
        private readonly string Secret;
        public OrderServices(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.key = _configuration["Razorpay:Key"];
            this.Secret = _configuration["Razorpay:Secret"];
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
    }
}
