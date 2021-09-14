// using System;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO vaultKeeps
      (vaultId, keepId, creatorId)
      VALUES
      (@VaultId, @KeepId, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
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