using hospital_ms.Core.ContributorAggregate;

namespace hospital_ms.UseCases.Contributors;
public record ContributorDto(ContributorId Id, ContributorName Name, PhoneNumber PhoneNumber);
