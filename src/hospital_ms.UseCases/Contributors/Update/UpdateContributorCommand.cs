using hospital_ms.Core.ContributorAggregate;

namespace hospital_ms.UseCases.Contributors.Update;

public record UpdateContributorCommand(ContributorId ContributorId, ContributorName NewName) : ICommand<Result<ContributorDto>>;
