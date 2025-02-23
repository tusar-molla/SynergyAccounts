using MediatR;
namespace SynergyAccounts.CQRS.Commands
{
    public record GenericCommand<T>(T Entity, string Operation) : IRequest<bool>;
}
