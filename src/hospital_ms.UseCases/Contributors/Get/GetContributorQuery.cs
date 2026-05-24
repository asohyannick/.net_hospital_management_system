using hospital_ms.Core.ContributorAggregate;

namespace hospital_ms.UseCases.Contributors.Get;

public record GetContributorQuery(ContributorId ContributorId) : IQuery<Result<ContributorDto>>;
