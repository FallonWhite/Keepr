using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _prorepo;
    private readonly AccountsRepository _accrepo;
    public ProfilesService(ProfilesRepository prorepo, AccountsRepository accrepo)
    {
      _prorepo = prorepo;
      _accrepo = accrepo;
    }

    internal Profile GetProfileById(string id)
    {
      Profile profile = _accrepo.GetById(id);
      if (profile == null)
      {
        throw new Exception("Id Not Valid");
      }
      return profile;
    }
  }

}