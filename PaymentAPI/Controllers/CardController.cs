using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Models;
using PaymentAPI.Services;

namespace PaymentAPI.Controllers
{
    [ApiController]
    [Route("api/card")]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService) => _cardService = cardService;


        [HttpPost("validate")]
        public IActionResult ValidateCard([FromBody] Card card)
        {
            if (_cardService.ValidateCard(card))
                return Ok(new { isValid = true });
            return BadRequest(new { isValid = false });
        }
    }
}
