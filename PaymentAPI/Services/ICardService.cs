using PaymentAPI.Models;

namespace PaymentAPI.Services
{
    public interface ICardService
    {
        bool ValidateCard(Card card);
    }
}
