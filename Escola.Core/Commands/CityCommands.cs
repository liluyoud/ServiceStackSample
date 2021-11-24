using Escola.Core.Entities;
using ServiceStack;

namespace Escola.Core.Commands;

[AutoApply(Behavior.AuditQuery)]
[Route("/cities", "GET")]
[Route("/cities/{Id}", "GET")]
public class QueryCitiesCommand : QueryDb<City>, IReturn<QueryResponse<City>>
{
    public long? Id { get; set; }
}

[ValidateHasRole("Manager")]
[AutoApply(Behavior.AuditCreate)]
[Route("/cities", "POST")]
public class CreateCityCommand : ICreateDb<City>, IReturn<IdResponse>
{
    public string? Name { get; set; }
    public string? Region { get; set; }
}

[ValidateHasRole("Manager")]
[AutoApply(Behavior.AuditModify)]
[Route("/cities/{Id}", "PATCH")]
public class UpdateCityCommand : IPatchDb<City>, IReturn<IdResponse>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Region { get; set; }
}

[ValidateHasRole("Manager")]
[AutoApply(Behavior.AuditSoftDelete)]
[Route("/cities/{Id}", "DELETE")]
public class DeleteCityCommand : IDeleteDb<City>, IReturn<IdResponse>
{
    public long Id { get; set; }
}

[ValidateHasRole("Manager")]
[AutoFilter(QueryTerm.Ensure, nameof(City.DeletedDate), Template = SqlTemplate.IsNotNull)]
[Route("/cities/deleted")]
public class DeletedCitiesCommand : QueryDb<City>, IReturn<QueryResponse<City>>
{

}