using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Api.Auth;
using TestTask.Api.Contracts;

namespace TestTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private const string AccessLevelClaim = "AccessLevel";

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: AuthController
        [HttpGet("info")]
        [Authorize(Policy = "MediumAccessLevel")]
        public ActionResult Index()
        {
            return Ok(new UserInfoResponse(User.FindFirst(AccessLevelClaim)?.Value));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            await _userService.Register(request.Login, request.Password, request.Agree);

            return Ok();
        }

        [HttpPost("step2")]
        [Authorize(Policy = "MediumAccessLevel")]
        public async Task<IActionResult> Step2([FromBody] LocationRequest request)
        {
            await _userService.CompleteStep2(User, request.Country, request.Province);

            return Ok();
        }
    }
}
