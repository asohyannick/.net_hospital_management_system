using hospital_ms.Core.ContributorAggregate;
using Vogen;

namespace hospital_ms.Infrastructure.Data.Config;

[EfCoreConverter<ContributorId>]
[EfCoreConverter<ContributorName>]
internal partial class VogenEfCoreConverters;
