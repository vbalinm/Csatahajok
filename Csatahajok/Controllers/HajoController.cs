using Csatahajok.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Csatahajok.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HajoController : ControllerBase
    {
        [HttpGet("All")]

        public IActionResult GetAll()
        {
            using (var context = new CsatahajokContext())
            {
                try
                {
                    var result = context.Hajos.ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Hiba a művelet végrehajtás közben!\n{ex.Message}");
                }
            }
        }

        [HttpGet("ByName/{name}")]

        public IActionResult GetByName(string name)
        {
            using (var context = new CsatahajokContext())
            {
                try
                {
                    var result = context.Hajos.FirstOrDefault(x => x.Nev == name);
                    if (result == null)
                        return NotFound($"Nincs ilyen nevű hajó: {name}");
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
