using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vaultKeepsService;

    public VaultKeepsController(VaultKeepsService vaultKeepsService)
    {
      _vaultKeepsService = vaultKeepsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeepAsync([FromBody] VaultKeep vaultKeepData)
    {
      try
      {
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        vaultKeepData.CreatorId = account.Id;
        return Ok(_vaultKeepsService.CreateVaultKeep(vaultKeepData, account.Id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteAsync(int id)
    {
      try
      {
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        _vaultKeepsService.Delete(id, account.Id);
        return Ok("Delete Successful");
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}