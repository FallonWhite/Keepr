using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;
using static Keepr.Models.Keep;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
      string sql = @"INSERT INTO vault_keeps
      (vaultId, keepId, creatorId)
      VALUES
      (@VaultId, @KeepId, @CreatorId);
      SELECT LAST_INSERT-ID();";
      int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
      vaultKeepData.Id = id;
      return vaultKeepData;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vault_keeps
      WHERE id = @id
      LIMIT 1;";
      _db.Execute(sql, new { id });
    }

    internal VaultKeep GetById(int id)
    {
      string sql = @"
      SELECT
      vk.*,
      a.*
      FROM vault_keeps vk
      JOIN accounts a on vk.creatorId = a.id
      WHERE vk.id = @id;";
      return _db.Query<VaultKeep, Account, VaultKeep>(sql, (VaultKeep, a) =>
      {
        VaultKeep.CreatorId = a.Id;
        return VaultKeep;
      }, new { id }).FirstOrDefault();
    }
  }
}