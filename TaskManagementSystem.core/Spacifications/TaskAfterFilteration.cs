using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;

namespace TaskManagementSystem.Core.Spacifications
{
    public class TaskAfterFilteration : Spacification<Taskat>
    {
        public TaskAfterFilteration(TaskParams param) : base(
              p => (string.IsNullOrEmpty(param.SearchItem) || p.Title.ToLower().Contains(param.SearchItem)) &&
            (!param.CategoryId.HasValue || p.CategoryId == param.CategoryId.Value)
            )
        {
        }

    }
}
