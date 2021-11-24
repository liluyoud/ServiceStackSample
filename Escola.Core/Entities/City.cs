using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Escola.Core.Entities;

[Alias("Cidades")]
public class City : AuditBase
{
    [AutoIncrement]
    public long Id { get; set; }

    [StringLength(100)]
    [Required]
    public string? Name { get; set; }

    [StringLength(2)]
    [Required]
    public string? Region { get; set; }

    [Reference]
    public List<School>? Schools { get; set; }
}
