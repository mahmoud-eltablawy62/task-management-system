using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Repository.Repo.Contract;

namespace TaskManagementSystem.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenaricRepo<TEntity> Repo<TEntity>() where TEntity : BaseClass;
        Task<int> CompleteAsync();
    }
}
