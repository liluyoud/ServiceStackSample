using Escola.Core.Entities;
using ServiceStack;

namespace Escola.Core.Commands;

[AutoApply(Behavior.AuditQuery)]
[Route("/schools", "GET")]
[Route("/schools/{Id}", "GET")]
public class QuerySchoolsCommand : QueryDb<School>, IReturn<QueryResponse<School>>
{
    public long? Id { get; set; }
}

[ValidateHasRole("Manager")]
[AutoApply(Behavior.AuditCreate)]
[Route("/schools", "POST")]
public class CreateSchoolCommand : ICreateDb<School>, IReturn<IdResponse>
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public long? CityId { get; set; }
    public string? Coordinate { get; set; }
    public string? Phone { get; set; }
    public string? CelPhone { get; set; }
    public string? WhatsApp { get; set; }
    public string? Email { get; set; }
    public string? Cnpj { get; set; }
    public bool Active { get; set; }
}

[ValidateHasRole("Manager")]
[AutoApply(Behavior.AuditModify)]
[Route("/schools/{Id}", "PATCH")]
public class UpdateSchoolCommand : IPatchDb<School>, IReturn<IdResponse>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public long? CityId { get; set; }
    public string? Coordinate { get; set; }
    public string? Phone { get; set; }
    public string? CelPhone { get; set; }
    public string? WhatsApp { get; set; }
    public string? Email { get; set; }
    public string? Cnpj { get; set; }
    public bool Active { get; set; }
}

[ValidateHasRole("Manager")]
[AutoApply(Behavior.AuditSoftDelete)]
[Route("/schools/{Id}", "DELETE")]
public class DeleteSchoolCommand : IDeleteDb<School>, IReturn<IdResponse>
{
    public long Id { get; set; }
}

[ValidateHasRole("Manager")]
[AutoFilter(QueryTerm.Ensure, nameof(School.DeletedDate), Template = SqlTemplate.IsNotNull)]
[Route("/schools/deleted")]
public class DeletedSchoolsCommand : QueryDb<School>, IReturn<QueryResponse<School>>
{

}