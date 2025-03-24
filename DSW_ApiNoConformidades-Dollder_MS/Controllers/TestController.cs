using Microsoft.AspNetCore.Mvc;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { mensaje = "¡Conexión exitosa desde el backend! 🎉" });
        }
    }

}
