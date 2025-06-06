using Microsoft.AspNetCore.Mvc;
using Tockify.Domain.Models;

namespace Tockify.WebAPI.Controllers
{

    /// <summary>
    /// 
    /// Controller for managing Client Users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientUserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<ClientUserModel>> GetAllClientUser()
        {
            return Ok();
        }
    }
}
