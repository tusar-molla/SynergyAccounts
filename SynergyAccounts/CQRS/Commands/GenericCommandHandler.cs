using MediatR;
using SynergyAccounts.Data;

namespace SynergyAccounts.CQRS.Commands
{
    public class GenericCommandHandler <T> :IRequestHandler<GenericCommand<T>, bool> where T :class
    {
        private readonly AppDbContext _context;

        public GenericCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public  async Task<bool> Handle(GenericCommand<T> request, CancellationToken cancellationToken)
        {
            switch (request.Operation)
            {
                case "Create":
                    _context.Set<T>().Add(request.Entity);
                    break;
                case "Update":
                    _context.Set<T>().Update(request.Entity);
                    break;
                case "Delete":
                    _context.Set<T>().Remove(request.Entity);
                    break;
                default:
                    return false;
            }
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }           
    }
}
