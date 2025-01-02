using Business.Interfaces;
using Business.Models;
using Business.Requests.Card;
using Microsoft.AspNetCore.Mvc;

namespace CardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController(ICardService cardService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GenerateCardAsync(InsertCardRequest card)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("", card);

            CardModel cardModel = await cardService.AddCardAsync(card);

            return Ok(cardModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetCardAsync(GetCardRequest card)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("", card);

            CardModel cardModel = await cardService.GetCardAsync(card);

            return Ok(cardModel);
        }
    }
}