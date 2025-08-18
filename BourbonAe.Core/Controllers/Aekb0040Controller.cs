using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BourbonAe.Core.Models.AEKB0040;
using BourbonAe.Core.Services.Features.AEKB0040;
namespace BourbonAe.Core.Controllers
{
    //[Authorize]
    public class Aekb0040Controller : Controller
    {
        private readonly IAekb0040Service _service;
        public Aekb0040Controller(IAekb0040Service service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Aekb0040Filter filter)
        {
            var rows = await _service.SearchAsync(filter);
            return View(new Aekb0040PageViewModel { Filter = filter, Rows = rows });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(Aekb0040PageViewModel vm)
        {
            var n = await _service.ApproveAsync(vm.SelectedApplyNos);
            TempData["Message"] = $"{n} 件を承認しました。";
            return RedirectToAction(nameof(Index), vm.Filter);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(Aekb0040PageViewModel vm)
        {
            var n = await _service.RejectAsync(vm.SelectedApplyNos);
            TempData["Message"] = $"{n} 件を却下しました。";
            return RedirectToAction(nameof(Index), vm.Filter);
        }
    }
}
