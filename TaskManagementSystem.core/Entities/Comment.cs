using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.core.Entities
{
    public class Comment : BaseClass
    {
        public string Text { get; set; }
        public DateTimeOffset TimeOfComment { get; set; } = DateTimeOffset.UtcNow;  
    }
}
