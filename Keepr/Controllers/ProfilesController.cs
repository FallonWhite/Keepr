using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _profilesService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;

    public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)
    {
      _profilesService = profilesService;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(String id)
    {
      try
      {
        Profile profile = _profilesService.GetProfileById(id);
        return Ok(profile);
      }
      catch (System.Exception err)
      {

        return BadRequest(err.Message);
      }
    }
    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<Keep>>> GetKeepsByProfileId(string id)
    {
      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
      try
      {
        List<Keep> keeps = _keepsService.GetKeepsByProfileId(id, userInfo);
        return Ok(keeps);
      }
      catch (System.Exception err)
      {

        return BadRequest(err.Message);
      }
    }
    [HttpGet("{id}/Vaults")]
    public async Task<ActionResult<List<Keep>>> GetVaultsByProfileId(string id)
    {
      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
      try
      {
        if (userInfo == null)
        {
          return Ok(_vaultsService.GetPublicVaults(id));
        }
        return Ok(_vaultsService.GetVaultsByProfileId(id));
      }
      catch (System.Exception err)
      {

        return BadRequest(err.Message);
      }
    }
  }
}