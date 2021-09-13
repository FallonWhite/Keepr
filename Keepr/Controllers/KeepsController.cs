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
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _keepsService;
    public KeepsController(KeepsService keepsService)
    {
      _keepsService = keepsService;
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAll()
    {
      try
      {
        return Ok(_keepsService.GetAll());
      }
      catch (System.Exception err)
      {

        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try
      {
        Keep keep = _keepsService.GetById(id);
        return Ok(keep);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    [Authorize]
    async public Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newKeep.CreatorId = userInfo.Id;
        Keep created = _keepsService.Create(newKeep);
        created.Creator = userInfo;
        return Ok(created);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    async public Task<ActionResult<Keep>> Update(int id, [FromBody] Keep newKeep)
    {
      try
      {
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        newKeep.Id = id;
        newKeep.CreatorId = account.Id;
        return Ok(_keepsService.Update(id, newKeep));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("/views/{id}")]
    public ActionResult<Keep> UpdateKeep(int id, [FromBody] Keep newKeep)
    {
      try
      {
        newKeep.CreatorId = null;
        newKeep.Id = id;
        newKeep.Name = null;
        newKeep.Description = null;
        newKeep.Img = null;
        return Ok(_keepsService.Update(id, newKeep));
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
        return Ok(_keepsService.Delete(id, account.Id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}