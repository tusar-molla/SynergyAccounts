using MediatR;
using Microsoft.EntityFrameworkCore;
using SynergyAccounts.Data;

namespace SynergyAccounts.CQRS.Queries
{
    public class GenericQueryHandler<T> : IRequestHandler<GenericQuery<T>, IEnumerable<T>> where T : class
    {
        private readonly AppDbContext _context;

        public GenericQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> Handle(GenericQuery<T> request, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }
    }
}
