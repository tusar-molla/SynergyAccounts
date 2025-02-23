using MediatR;

namespace SynergyAccounts.CQRS.Queries
{
    public record GenericQuery<T>() : IRequest<IEnumerable<T>>;
}
