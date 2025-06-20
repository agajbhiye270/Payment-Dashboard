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
            var result = _cardService.ValidateCard(card);

            if (result.IsValid)
                return Ok(new { isValid = true, message = result.Message });

            return BadRequest(new { isValid = false, message = result.Message });
        }
    }
}
