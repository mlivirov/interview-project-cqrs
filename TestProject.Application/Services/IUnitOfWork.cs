using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Domain.Models;

namespace TestProject.Application.Services
{
    public interface IUnitOfWork
    {
        Task AddAsync(IModel model, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}