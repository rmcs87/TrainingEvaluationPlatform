using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TEP.Domain.Entities;

namespace TEP.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Asset> Assets { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
