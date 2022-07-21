using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using knightstale.Models;

namespace knightstale.Repositories
{
  public class CastlesRepository
  {
    private readonly IDbConnection _db;

    public CastlesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Castle> Get()
    {
      string sql = @"
            SELECT
            c.*,
            a.*
            FROM castles c
            JOIN accounts a ON a.id = c.creatorId
            ";
      return _db.Query<Castle, Profile, Castle>(sql, (castle, profile) =>
      {
        castle.Creator = profile;
        return castle;
      }).ToList();
    }
    internal Castle Get(int id)
    {
      string sql = @"
        SELECT
        c.*,
        a.*
        FROM castles c
        JOIN accounts a ON a.id = c.creatorId
        WHERE arts.id = @id
        ";
      return _db.Query<Castle, Profile, Castle>(sql, (castle, profile) =>
      {
        castle.Creator = profile;
        return castle;
      }, new { id }).FirstOrDefault();
    }

    internal Castle Create(Castle castleData)
    {
      string sql = @"
        INSERT INTO castles
        (name, location, creatorId)
        VALUES
        (@Name, @Location, @CreatorId);
        SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, castleData);
      castleData.Id = id;
      return castleData;
    }

    internal void Edit(Castle original)
    {
      string sql = @"
        UPDATE castles
        SET
        name = @Name,
        location = @Location
        WHERE id = @Id
        ";
      _db.Execute(sql, original);
    }
    internal void Delete(int id)
    {
      string sql = "DELETE FROM castles WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}