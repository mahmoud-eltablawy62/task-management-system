using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagementSystem.Repository.Data;
using TaskManagementSystem.Core;
using TaskManagementSystem.Repository.Repo.Contract;
using Talabat.Repository.Data;
using TaskManagementSystem.core.Entities;

namespace TaskManagementSystem.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TasksContext context;

        private Hashtable _Repo;

        public UnitOfWork(TasksContext _context)
        {
            context = _context;
            _Repo = new Hashtable();
        }

        public Task<int> CompleteAsync() =>
            context.SaveChangesAsync();


        public ValueTask DisposeAsync() =>
           context.DisposeAsync();


        public IGenaricRepo<TEntity> Repo<TEntity>() where TEntity : BaseClass
        {
            var key = typeof(TEntity).Name;

            if (!_Repo.ContainsKey(key))
            {
                var repo = new GenaricRepo <TEntity>(context);

                _Repo.Add(key, repo);
            }
            return _Repo[key] as IGenaricRepo<TEntity>;
        }

       
    }
}
