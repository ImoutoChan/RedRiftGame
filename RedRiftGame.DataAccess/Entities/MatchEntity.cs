using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace RedRiftGame.DataAccess.Entities;

internal class MatchEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string HostName { get; set; } = default!;

    [Required]
    public string GuestName { get; set; } = default!;
    
    public bool IsHostWinner { get; set; }

    public int HostFinalHealth { get; set; }

    public int GuestFinalHealth { get; set; }

    public int TotalTurnsPlayed { get; set; }

    public Instant StartedAt { get; set; }
    
    public Instant FinishedAt { get; set; }
}
