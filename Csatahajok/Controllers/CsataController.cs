using Csatahajok.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Csatahajok.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsataController : ControllerBase
    {
        [HttpGet("Resztvevok/{name}")]

        public IActionResult GetResztvevok(string name)
        {
            using (var context = new CsatahajokContext())
            {
                try
                {
                    var result = context.Kimenets.Include(x => x.HajoNavigation).Include(x => x.CsataNavigation).Where(x => x.CsataNavigation.Nev == name).Select(x => x.HajoNavigation.Nev).ToList();
                    if (result.Count == 0)
                        return StatusCode(204, "Nincs megfelelő hajó.");
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Hiba a kérés teljesítése közben!\n{ex.Message}");
                }
            }
        }
    }
}
