using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, isPrivate, creatorId)
      VALUES (@Name, @Description, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVault);
    }
    internal Vault GetById(int id)
    {
      string sql = @"
      SELECT 
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.id = @id;";
      return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { id }).FirstOrDefault();
    }

    internal int Update(Vault update)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @Name,
      description = @Description,
      isPrivate = @IsPrivate
      WHERE id = @Id;";
      return _db.Execute(sql, update);
    }

    internal int Delete(int id)
    {
      string sql = @"
      DELETE FROM vaults
      WHERE id = @id;";
      return _db.Execute(sql, new { id });
    }

    internal List<Vault> GetAll()
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id;";
      return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, splitOn: "id").ToList();
    }

    public List<Vault> GetVaultsByProfileId(string id)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE a.id = @id;";
      return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { id }).ToList();
    }
  }
}