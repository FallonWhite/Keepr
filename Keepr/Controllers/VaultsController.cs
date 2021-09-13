using System.Collections.Generic;
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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;
    public VaultsController(VaultsService vaultsService, KeepsService keepsService)
    {
      _vaultsService = vaultsService;
      _keepsService = keepsService;
    }

    [HttpGet]
    public ActionResult<List<Vault>> GetAll()
    {
      try
      {
        return Ok(_vaultsService.GetAll());
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]
    async public Task<ActionResult<Vault>> GetById(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault = _vaultsService.GetById(id);
        if (vault.IsPrivate != true)
        {
          return Ok(vault);
        }
        else if (userInfo.Id == vault.CreatorId)
        {
          return Ok(vault);
        }
        throw new System.Exception("Not Available");
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    [Authorize]
    async public Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVault.CreatorId = userInfo.Id;
        Vault created = _vaultsService.Create(newVault);
        created.Creator = userInfo;
        return Ok(created);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut]
    [Authorize]
    async public Task<ActionResult<Vault>> Update(int id, [FromBody] Vault newVault)
    {
      try
      {
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        newVault.CreatorId = account.Id;
        newVault.Id = id;
        return Ok(_vaultsService.Update(id, newVault));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    async public Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_vaultsService.Delete(id, account.Id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    async public Task<ActionResult<IEnumerable<VaultKeepViewModel>>> GetKeepsByVaultId(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        if (userInfo == null)
        {
          return Ok(_keepsService.GetKeepsByVaultId(id));
        }
        return Ok(_keepsService.GetKeepsByVaultId(id, userInfo));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}