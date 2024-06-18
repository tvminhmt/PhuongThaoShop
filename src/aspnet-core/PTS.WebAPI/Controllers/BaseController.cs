using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PTS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "admin")]
    [AllowAnonymous]
    public class BaseController : ControllerBase
    {
        private ISender _mediator = null;
        private IConfiguration _configuration = null;
        protected IConfiguration configuration => _configuration ??= HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        protected ISender mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
