namespace E_CommerceFurnitureBackend.Services.JwtServices
{
    public interface IJwtServices
    {
        Task<int> GetUserIdFromToken(string token);
        //Task<string> GetToke();
    }
}
