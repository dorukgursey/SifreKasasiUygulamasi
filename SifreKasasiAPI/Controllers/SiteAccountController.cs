using BusinessLayer.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace SifreKasasiAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SiteAccountsController : ControllerBase
    {
        private readonly ISiteAccountService _siteAccountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public SiteAccountsController(ISiteAccountService siteAccountService, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _siteAccountService = siteAccountService;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet("GetSiteAccount/{id}")]
        public IActionResult GetSiteAccount(int id)
        {
            var siteAccount = _siteAccountService.GetSiteAccountById(id);
            if (siteAccount == null)
                return NotFound();

            return Ok(siteAccount);
        }

        [HttpGet("GetAllSiteAccounts")]
        public IActionResult GetAllSiteAccounts()
        {
            var siteAccounts = _siteAccountService.GetAllSiteAccounts();
            return Ok(siteAccounts);
        }

        [HttpPost("CreateSiteAccount")]
        public IActionResult CreateSiteAccount([FromBody] SiteAccount siteAccount)
        {
            _siteAccountService.CreateSiteAccount(siteAccount);
            return CreatedAtAction("GetSiteAccount", new { id = siteAccount.SiteId }, siteAccount);
        }

        [HttpPut("UpdateSiteAccount/{id}")]
        public IActionResult UpdateSiteAccount(int id, [FromBody] SiteAccount siteAccount)
        {
            if (id != siteAccount.SiteId)
                return BadRequest();

            _siteAccountService.UpdateSiteAccount(siteAccount);
            return NoContent();
        }

        [HttpDelete("DeleteSiteAccount/{id}")]
        public IActionResult DeleteSiteAccount(int id)
        {
            _siteAccountService.DeleteSiteAccount(id);
            return NoContent();
        }
        [HttpGet("GetCurrentUser")]
        public IActionResult GetCurrentUser()
        {
            var currentUser = CurrentUser();

            return Ok(currentUser);
        }
        private AppUser CurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new AppUser
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                };
            }
            return null;

        }
    }

}
