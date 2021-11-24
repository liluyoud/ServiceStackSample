using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Escola.Core.Entities;

[Alias("Escolas")]
public class School : AuditBase
{
    [AutoIncrement]
    public long Id { get; set; }

    [StringLength(100)]
    [Required]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    [Reference]
    public City? City { get; set; }
    public long? CityId { get; set; }

    [StringLength(100)]
    public string? Coordinate { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }

    [StringLength(50)]
    public string? CelPhone { get; set; }

    [StringLength(100)]
    public string? WhatsApp { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(18)]
    public string? Cnpj { get; set; }

    public bool Active { get; set; }

}
