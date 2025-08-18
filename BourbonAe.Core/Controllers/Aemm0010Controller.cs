using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BourbonAe.Core.Models.AEMM0010;
using BourbonAe.Core.Services.Features.AEMM0010;
namespace BourbonAe.Core.Controllers
{
    //[Authorize]
    public class Aemm0010Controller : Controller
    {
        private readonly IAemm0010Service _service;
        public Aemm0010Controller(IAemm0010Service service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Aemm0010Filter filter)
        {
            var vm = new Aemm0010PageViewModel { Filter = filter, Rows = await _service.SearchAsync(filter) };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? code)
        {
            if (string.IsNullOrWhiteSpace(code)) return View(new Aemm0010EditRow());
            var row = await _service.FindAsync(code);
            if (row is null) return NotFound();
            return View(row);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Aemm0010EditRow row)
        {
            if (!ModelState.IsValid) return View(row);
            await _service.UpsertAsync(row);
            TempData["Message"] = "保存しました。";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string code)
        {
            var n = await _service.DeleteAsync(code);
            TempData["Message"] = n > 0 ? "削除しました。" : "対象が見つかりません。";
            return RedirectToAction(nameof(Index));
        }
    }
}
