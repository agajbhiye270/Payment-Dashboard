using PaymentAPI.Models;

namespace PaymentAPI.Services
{
    public class CardService:ICardService
    {
            public (bool IsValid, string Message) ValidateCard(Card card)
            {
                if (string.IsNullOrWhiteSpace(card.CardNumber) || card.CardNumber.Length != 16)
                    return (false, "Card number must be exactly 16 digits.");

                if (!card.CardNumber.All(char.IsDigit))
                    return (false, "Card number must contain digits only.");

                if (string.IsNullOrWhiteSpace(card.CVV) || card.CVV.Length != 3)
                    return (false, "CVV must be exactly 3 digits.");

                if (!card.CVV.All(char.IsDigit))
                    return (false, "CVV must contain digits only.");

                // Additional checks can go here

                return (true, "Card is valid.");
            }
        

    }
}

