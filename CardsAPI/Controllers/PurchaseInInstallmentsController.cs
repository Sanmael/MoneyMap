using Business.Interfaces;
using Business.Requests.Card.PurchaseInInstallments;
using Business.Response;
using Microsoft.AspNetCore.Mvc;

namespace CardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseInInstallmentsController(IPurchaseInInstallmentsService service) : ControllerBase
    {
        [HttpPost("GeneratePurchaseInInstallments")]
        public async Task<IActionResult> GeneratePurchaseInInstallmentsAsync(InsertPurchaseInInstallmentsRequest insertPurchaseInInstallmentsRequest)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("", insertPurchaseInInstallmentsRequest);

            BaseResponse addPurchaseInInstallmentsResponse = await service.AddPurchaseInInstallmentsAsync(insertPurchaseInInstallmentsRequest);

            if (!addPurchaseInInstallmentsResponse.Success)
                return BadRequest(addPurchaseInInstallmentsResponse);

            return Ok(addPurchaseInInstallmentsResponse);
        }

        [HttpGet("GetPurchaseInInstallments")]
        public async Task<IActionResult> GetPurchaseInInstallmentsAsync(GetPurchaseInInstallmentsRequest getPurchaseInInstallmentsRequest)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("", getPurchaseInInstallmentsRequest);

            BaseResponse getPurchaseInInstallmentsResponse = await service.GetPurchaseInInstallments(getPurchaseInInstallmentsRequest);

            if (!getPurchaseInInstallmentsResponse.Success)
                return BadRequest(getPurchaseInInstallmentsResponse);

            return Ok(getPurchaseInInstallmentsResponse);
        }

        [HttpGet("GetPurchaseInInstallmentsListActive")]
        public async Task<IActionResult> GetPurchaseInInstallmentsListActiveAsync([FromQuery] long cardId, [FromQuery] long userId)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("", userId);

            BaseResponse? getPurchaseInInstallmentsListActiveResponse = await service.GetPurchaseInInstallmentsListActive(cardId, userId);

            if (!getPurchaseInInstallmentsListActiveResponse.Success)
                return BadRequest(getPurchaseInInstallmentsListActiveResponse);

            return Ok(getPurchaseInInstallmentsListActiveResponse);
        }
    }
}