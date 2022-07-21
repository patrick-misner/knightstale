using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using knightstale.Models;
using knightstale.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace knightstale.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class CastlesController : ControllerBase
  {
    private readonly CastlesService _cServ;

    public CastlesController(CastlesService cServ)
    {
      _cServ = cServ;
    }

    [HttpGet]
    public ActionResult<List<Castle>> Get()
    {
      try
      {
        List<Castle> castles = _cServ.Get();
        return Ok(castles);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Castle> Get(int id)
    {
      try
      {
        Castle castle = _cServ.Get(id);
        return Ok(castle);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Castle>> Create([FromBody] Castle castleData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        castleData.CreatorId = userInfo.Id;
        Castle newCastle = _cServ.Create(castleData);
        newCastle.Creator = userInfo;
        return Ok(newCastle);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    public ActionResult<Castle> Edit(int id, [FromBody] Castle castleData)
    {
      Castle update = _cServ.Edit(castleData);
      return Ok(update);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Castle>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Castle deletedCastle = _cServ.Delete(id, userInfo.Id);
        return Ok(deletedCastle);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}