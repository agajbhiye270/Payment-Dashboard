namespace PaymentAPI.Services
{
    public interface ITokenService
    {
        string GenerateToken(string username);
        bool ValidateTestUser(string username, string password);
    }
}
