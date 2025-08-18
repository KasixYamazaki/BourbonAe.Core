using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BourbonAe.Core.Models.AEST0020;
using BourbonAe.Core.Services.Features.AEST0020;

namespace BourbonAe.Core.Controllers
{
    //[Authorize]
    public class Aest0020Controller : Controller
    {
        private readonly IAest0020Service _service;
        public Aest0020Controller(IAest0020Service service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Aest0020Filter filter)
        {
            var rows = await _service.SearchAsync(filter);
            return View(new Aest0020PageViewModel { Filter = filter, Rows = rows });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return View(new Aest0020EditRow());
            var row = await _service.FindAsync(id.Value);
            if (row is null) return NotFound();
            return View(row);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Aest0020EditRow row)
        {
            if (!ModelState.IsValid) return View(row);
            await _service.UpsertAsync(row);
            TempData["Message"] = "保存しました。";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var n = await _service.DeleteAsync(id);
            TempData["Message"] = n > 0 ? "削除しました。" : "対象が見つかりません。";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkStatus(Aest0020PageViewModel vm, string newStatus)
        {
            var n = await _service.BulkUpdateStatusAsync(vm.SelectedIds, newStatus);
            TempData["Message"] = $"{n} 件の状態を「{newStatus}」に更新しました。";
            return RedirectToAction(nameof(Index), vm.Filter);
        }
    }
}
