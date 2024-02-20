using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;

namespace TaskManagementSystem.Core.Services.Contract
{
    public interface IOrderService
    {
        Task<Taskat?> CreateTaskAsync();
    }
}
