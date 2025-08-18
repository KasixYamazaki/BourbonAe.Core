using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BourbonAe.Core.Models.AESJ1110;
using BourbonAe.Core.Services.Features.AESJ1110;
namespace BourbonAe.Core.Controllers
{
    //[Authorize]
    public class Aesj1110Controller : Controller
    {
        private readonly IAesj1110Service _service;
        public Aesj1110Controller(IAesj1110Service service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Aesj1110Filter filter)
        {
            var rows = await _service.SearchAsync(filter);
            return View(new Aesj1110PageViewModel { Filter = filter, Rows = rows });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Aesj1110PageViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Rows = await _service.SearchAsync(vm.Filter);
                return View(vm);
            }
            var count = await _service.SaveAsync(vm.SelectedSlipNos);
            TempData["Message"] = $"{count} 件を保存しました。";
            return RedirectToAction(nameof(Index), new
            {
                vm.Filter.CustomerCode,
                FromDate = vm.Filter.FromDate?.ToString("yyyy-MM-dd"),
                ToDate = vm.Filter.ToDate?.ToString("yyyy-MM-dd"),
                vm.Filter.Status
            });
        }
    }
}
