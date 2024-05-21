using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Services.OrderServices
{
    public interface IOrderServices
    {
         Task<MerchantOrder> GenerateOrder(PaymentDto payment);
         Task<string> CapturePayment(IHttpContextAccessor _httpContextAccessor);
        Task<List<OrderDto>> OrderDetails(string token);
    }
}
