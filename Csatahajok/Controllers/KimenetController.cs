using Csatahajok.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Csatahajok.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KimenetController : ControllerBase
    {
        [HttpPost("UjKimenet")]
        public async Task<IActionResult> UjKimenet(Kimenet ujKimenet)
        {
            using (var context = new CsatahajokContext())
            {
                try
                {
                    await context.Kimenets.AddAsync(ujKimenet);
                    await context.SaveChangesAsync();
                    return Ok("A kimenet sikeresen rögzítve.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Hiba rögzítés közben!\n{ex.Message}");
                }
            }
        }

        [HttpDelete("KimenetTorles/{csata}/{hajonev}")]
        public async Task<IActionResult> TorolKimenet(string csata, string hajonev)
        {
            using (var context = new CsatahajokContext())
            {
                try
                {
                    var result = context.Kimenets.FirstOrDefault(x => x.Hajo == hajonev && x.Csata == csata);
                    if (result == null)
                        return NotFound($"Nincs megfelelő csata vagy hajó: {csata}, {hajonev}");

                    context.Kimenets.Remove(result);
                    await context.SaveChangesAsync();
                    return Ok("A kimenet sikeresen törölve.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Hiba a törlés közben!\n{ex.Message}");
                }
            }
        }
    }
}
