using hospital_ms.Core.ContributorAggregate;

namespace hospital_ms.UseCases.Contributors.Delete;

public record DeleteContributorCommand(ContributorId ContributorId) : ICommand<Result>;
