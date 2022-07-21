using System;
using System.Collections.Generic;
using knightstale.Models;
using knightstale.Repositories;

namespace knightstale.Services
{
  public class CastlesService
  {
    private readonly CastlesRepository _repo;

    public CastlesService(CastlesRepository repo)
    {
      _repo = repo;
    }
    internal List<Castle> Get()
    {
      return _repo.Get();
    }


    internal Castle Get(int id)
    {
      Castle found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }


    internal Castle Create(Castle castleData)
    {
      return _repo.Create(castleData);
    }


    internal Castle Edit(Castle castleData)
    {
      Castle original = Get(castleData.Id);
      original.Name = castleData.Name ?? original.Name;
      original.Location = castleData.Location ?? original.Location;

      _repo.Edit(original);

      return original;
    }


    internal Castle Delete(int id, string userId)
    {
      Castle original = Get(id);
      _repo.Delete(id);
      return original;
    }
  }
}