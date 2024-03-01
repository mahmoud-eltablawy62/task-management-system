using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;

namespace TaskManagementSystem.Core.Spacifications
{
    public class Spacification<T> : ISpacification<T> where T : BaseClass
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> TaskatBy { get; set; } = null;

        public Expression<Func<T, object>> TaskatByDesc { get; set; } = null;
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagination { get; set; }

        public Spacification()
        {
        }

        public Spacification(Expression<Func<T, bool>> _Criteria)
        {
            Criteria = _Criteria;
            Includes = new List<Expression<Func<T, object>>>();
        }

        public void AddOrderBy(Expression<Func<T, object>> TaskByExp)
        {
            TaskatBy = TaskByExp;
        }

        

        public void Pagination(int skip, int take)
        {
            IsPagination = true;
            Skip = skip;
            Take = take;
        }
    }
}
