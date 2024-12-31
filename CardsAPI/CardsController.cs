using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardsAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController(ICardService cardService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GenerateCardAsync(CardModel card)
        {
            if (ModelState.IsValid)
                return RedirectToAction("", card);

            await cardService.AddCardAsync(card);

            return Ok();
        }
    }
}