using BusinessLayer.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SifreKasasiAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SiteAccountsController : ControllerBase
    {
        private readonly ISiteAccountService _siteAccountService;

        public SiteAccountsController(ISiteAccountService siteAccountService)
        {
            _siteAccountService = siteAccountService;
        }

        [HttpGet("{id}")]
        public IActionResult GetSiteAccount(int id)
        {
            var siteAccount = _siteAccountService.GetSiteAccountById(id);
            if (siteAccount == null)
                return NotFound();

            return Ok(siteAccount);
        }

        [HttpGet]
        public IActionResult GetAllSiteAccounts()
        {
            var siteAccounts = _siteAccountService.GetAllSiteAccounts();
            return Ok(siteAccounts);
        }

        [HttpPost]
        public IActionResult CreateSiteAccount([FromBody] SiteAccount siteAccount)
        {
            _siteAccountService.CreateSiteAccount(siteAccount);
            return CreatedAtAction("GetSiteAccount", new { id = siteAccount.SiteId }, siteAccount);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSiteAccount(int id, [FromBody] SiteAccount siteAccount)
        {
            if (id != siteAccount.SiteId)
                return BadRequest();

            _siteAccountService.UpdateSiteAccount(siteAccount);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSiteAccount(int id)
        {
            _siteAccountService.DeleteSiteAccount(id);
            return NoContent();
        }
    }

}
