using Business.Interfaces;
using Business.Models;
using Business.Requests.Card.PurchaseInInstallments;
using Microsoft.AspNetCore.Mvc;

namespace CardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseInInstallmentsController(IPurchaseInInstallmentsService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GeneratePurchaseInInstallmentsAsync(InsertPurchaseInInstallmentsRequest insertPurchaseInInstallmentsRequest)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("", insertPurchaseInInstallmentsRequest);

            PurchaseInInstallmentsModel purchaseInInstallmentsModel = await service.AddPurchaseInInstallmentsAsync(insertPurchaseInInstallmentsRequest);

            return Ok(purchaseInInstallmentsModel);
        }
    }
}