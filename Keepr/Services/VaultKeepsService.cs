using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkrepo;
    private readonly KeepsRepository _keepsrepo;
    private readonly VaultsRepository _vaultsrepo;
    public VaultKeepsService(VaultKeepsRepository vkrepo, KeepsRepository keepsrepo, VaultsRepository vaultsrepo)
    {
      _vkrepo = vkrepo;
      _keepsrepo = keepsrepo;
      _vaultsrepo = vaultsrepo;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep newVaultKeep)
    {
      // VaultKeep newVaultKeep = _vkrepo.CreateVaultKeep(newVaultkeep);
      // newVaultkeep.Id = vaultKeep.Id;
      Vault vault = _vaultsrepo.GetById(newVaultKeep.VaultId);
      if (vault.CreatorId == newVaultKeep.CreatorId)
      {
        return _vkrepo.CreateVaultKeep(newVaultKeep);
      }
      throw new Exception("Cannot Create");
    }

    internal void Delete(int id, string userId)
    {
      VaultKeep vaultKeep = GetById(id);
      if (vaultKeep.CreatorId == userId)
      {
        _vkrepo.Delete(id);
      }
      throw new Exception("Delete What Isn't Yours You Cannot");
    }

    public VaultKeep GetById(int id)
    {
      VaultKeep vaultKeep = _vkrepo.GetById(id);
      if (vaultKeep == null)
      {
        throw new Exception("Id Not Valid");
      }
      return vaultKeep;
    }
  }
}