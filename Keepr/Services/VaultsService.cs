using System;
using System.Collections.Generic;
using System.Linq;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultsrepo;
    public VaultsService(VaultsRepository vaultsrepo)
    {
      _vaultsrepo = vaultsrepo;
    }

    internal Vault Create(Vault newVault)
    {
      int id = _vaultsrepo.Create(newVault);
      newVault.Id = id;
      return newVault;
    }

    internal List<Vault> GetAll()
    {
      return _vaultsrepo.GetAll();
    }
    public Vault GetById(int id)
    {
      Vault vault = _vaultsrepo.GetById(id);
      if (vault == null)
      {
        throw new Exception("Id Not Valid");
      }
      return vault;
    }

    // internal Vault Update(int id, Vault newVault)
    // {
    //   Vault vault = GetById(id);
    //   if (vault.CreatorId == newVault.CreatorId)
    //   {
    //     vault.Name = newVault.Name != null ? newVault.Name : vault.Name;
    //     vault.Description = newVault.Description != null ? newVault.Description : vault.Description;
    //     vault.IsPrivate = newVault.IsPrivate;
    //     if (_vaultsrepo.Update(newVault) > 0)
    //     {
    //       return newVault;
    //     }
    //     throw new Exception("Cannot Update");
    //   }
    //   throw new Exception("Cannot Change What Isn't Yours");
    // }
    internal Vault Update(string userId, Vault newVault)
    {
      Vault original = GetById(newVault.Id);
      if (original.CreatorId != newVault.CreatorId)
      {
        throw new Exception("Cannot Change What Isn't Yours");
      }
      original.Name = newVault.Name ?? original.Name;
      original.Description = newVault.Description ?? original.Description;
      original.IsPrivate = newVault.IsPrivate ?? original.IsPrivate;
      _vaultsrepo.Update(original);
      return original;
    }

    internal string Delete(int id, string userId)
    {
      Vault vault = GetById(id);
      if (vault.CreatorId == userId)
      {
        if (_vaultsrepo.Delete(id) > 0)
        {
          return "Poof, Gone Just Like That!";
        }
        return "Invalid Id";
      }
      throw new Exception("Delete What Isn't Yours You Cannot");
    }

    internal List<Vault> GetPublicVaults(string id)
    {
      List<Vault> profileVaults = _vaultsrepo.GetVaultsByProfileId(id);
      List<Vault> newProfileVaults = profileVaults.Where(v => v.IsPrivate != true).ToList();
      return newProfileVaults;
    }

    internal object GetVaultsByProfileId(string id)
    {
      List<Vault> profileVaults = _vaultsrepo.GetVaultsByProfileId(id);
      List<Vault> newProfileVaults = profileVaults.Where(v => v.CreatorId == id).ToList();
      return newProfileVaults;
    }
  }
}