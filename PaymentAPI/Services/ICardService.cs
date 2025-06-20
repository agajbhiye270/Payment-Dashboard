using PaymentAPI.Models;

namespace PaymentAPI.Services
{
    public interface ICardService
    {
        public (bool IsValid, string Message) ValidateCard(Card card);
    }
}