using Business.Interfaces;
using Business.Requests.Card;
using Business.Response;
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

            BaseResponse response = await cardService.AddCardAsync(card);

            if(!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetCardAsync(GetCardRequest card)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("", card);

            BaseResponse response = await cardService.GetCardAsync(card);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}