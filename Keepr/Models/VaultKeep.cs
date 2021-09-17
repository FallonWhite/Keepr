using System.ComponentModel.DataAnnotations;
namespace Keepr.Models
{
  public class VaultKeep
  {
    public Vault vault { get; set; }
    public Keep keep { get; set; }
    public int Id { get; set; }
    public string CreatorId { get; set; }
    [Required]
    public int VaultId { get; set; }
    public int KeepId { get; set; }
  }
}