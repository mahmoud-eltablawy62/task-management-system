using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Core.Entities.Identity
{
    public class User : IdentityUser
    {
      public string Name {  get; set; }    
    }
}
