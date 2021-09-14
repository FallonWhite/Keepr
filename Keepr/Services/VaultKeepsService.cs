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

    public VaultKeep CreateVaultKeep(VaultKeep vaultkeepData, string userId)
    {
      VaultKeep vaultKeep = _vkrepo.CreateVaultKeep(vaultkeepData);
      vaultkeepData.Id = vaultKeep.Id;
      Vault vault = _vaultsrepo.GetById(vaultkeepData.VaultId);
      if (vault.CreatorId != userId)
      {
        throw new Exception("Cannot Create What Isn't Yours");
      }
      return vaultkeepData;
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