using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;

namespace TaskManagementSystem.Core.Spacifications.TaskSpacificarion
{
    public class TaskWithCategory : Spacification<Taskat>
    {
        public TaskWithCategory(TaskParams Param) : base(
             p =>
             (string.IsNullOrEmpty(Param.SearchItem) || p.Title.ToLower().Contains(Param.SearchItem)) &&
             (!Param.CategoryId.HasValue || p.CategoryId == Param.CategoryId.Value)
            ) {
            Adds();
            if (!string.IsNullOrEmpty(Param.Sort))
            {
                AddOrderBy(p => p.Title);
            }
            else
            {
                AddOrderBy(p => p.Title);
            };
            Pagination((Param.PageIndex - 1) * Param.PageSize, Param.PageSize);
        }
        public TaskWithCategory(int id) : base(B => B.Id == id)
        {
            Adds();
        }
        private void Adds()
        {
            Includes.Add(B => B.Category);
        }
    }
}
