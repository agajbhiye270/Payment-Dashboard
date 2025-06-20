using PaymentAPI.Models;

namespace PaymentAPI.Services
{
    public class CardService:ICardService
    {
        public bool ValidateCard(Card card)
        {
            return card.CardNumber.Length == 16;
                
            //&& card.CVV.Length == 3;
        }
    }
}
