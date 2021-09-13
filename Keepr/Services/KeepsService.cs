using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _keepsrepo;
    private readonly VaultKeepsRepository _vkrepo;
    private readonly VaultsRepository _vaultsrepo;
    public KeepsService(KeepsRepository keepsrepo, VaultKeepsRepository vkrepo, VaultsRepository vaultsrepo)
    {
      _keepsrepo = keepsrepo;
      _vkrepo = vkrepo;
      _vaultsrepo = vaultsrepo;
    }

    internal Keep Create(Keep newKeep)
    {
      newKeep.Id = _keepsrepo.Create(newKeep);
      return newKeep;
    }

    internal List<Keep> GetAll()
    {
      return _keepsrepo.GetAll();
    }
    public Keep GetById(int id)
    {
      Keep keep = _keepsrepo.GetById(id);
      if (keep != null)
      {
        return keep;
      }
      throw new Exception("Keep Not Found");
    }

    internal Keep Update(int id, Keep newKeep)
    {
      Keep keep = GetById(id);
      if (keep.CreatorId == newKeep.CreatorId)
      {
        newKeep.Name = newKeep.Name != null ? newKeep.Name : keep.Name;
        newKeep.Description = newKeep.Description != null ? newKeep.Description : keep.Description;
        newKeep.Img = newKeep.Img != null ? newKeep.Img : keep.Img;
        newKeep.Views = newKeep.Views == (keep.Views += 1) ? newKeep.Views : keep.Views;
        newKeep.Keeps = newKeep.Keeps == (keep.Keeps += 1) ? newKeep.Keeps : keep.Keeps;
        if (_keepsrepo.Update(newKeep) > 0)
        {
          return newKeep;
        }
        throw new Exception("Cannot Update Keep");
      }
      throw new Exception("Can Only Update Your Own Keep - Get Your Own!");
    }
    public List<Keep> GetKeepsByProfileId(string id, Account userInfo)
    {
      var keeps = _keepsrepo.GetKeepsByProfileId(id);
      return keeps;
    }

    internal string Delete(int id, string userId)
    {
      Keep keep = GetById(id);
      if (keep.CreatorId == userId)
      {
        if (_keepsrepo.Delete(id) > 0)
        {
          return "Poof! No More Keeps Kept";
        }
        return "Invalid Id";
      }
      throw new Exception("This Isn't The Keep You're Looking For");
    }

    internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int id, Account userInfo)
    {
      var vault = _vaultsrepo.GetById(id);
      if (vault.IsPrivate == true)
      {
        if (vault.CreatorId == userInfo.Id)
        {
          return _keepsrepo.GetKeepsByVaultId(id);
        }
        throw new Exception("Private! Cannot Access");
      }
      return _keepsrepo.GetKeepsByVaultId(id);
    }

    internal object GetKeepsByVaultId(int id)
    {
      var vault = _vaultsrepo.GetById(id);
      if (vault.IsPrivate == true)
      {
        throw new Exception("Private! Cannot Access");
      }
      return _keepsrepo.GetKeepsByVaultId(id);
    }
  }
}