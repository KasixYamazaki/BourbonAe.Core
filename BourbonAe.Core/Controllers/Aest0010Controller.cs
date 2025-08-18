using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BourbonAe.Core.Models.AEST0010;
using BourbonAe.Core.Services.Features.AEST0010;
namespace BourbonAe.Core.Controllers
{
    //[Authorize]
    public class Aest0010Controller : Controller
    {
        private readonly IAest0010Service _service;

        public Aest0010Controller(IAest0010Service s) => _service = s;

        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> Index([FromQuery] Aest0010Filter f)
        {
            var rows = await _service.SearchAsync(f);

            return View(new Aest0010PageViewModel
            {
                Filter = f,
                Rows = rows
            });
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return View(new Aest0010EditRow());

            var row = await _service.FindAsync(id.Value);
            return row is null ? NotFound() : View(row);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> Edit(Aest0010EditRow row)
        {
            if (!ModelState.IsValid)
                return View(row);

            await _service.UpsertAsync(row);
            TempData["Message"] = "保存しました。";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> Delete(int id)
        {
            var n = await _service.DeleteAsync(id);

            TempData["Message"] = n > 0
                ? "削除しました。"
                : "対象が見つかりません。";

            return RedirectToAction(nameof(Index));
        }
    }
}
