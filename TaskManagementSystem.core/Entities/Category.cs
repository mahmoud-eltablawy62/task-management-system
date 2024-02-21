using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.core.Entities
{
    public class Category : BaseClass
    {
        public Category() { }
        public Category(string name , string description)
        {
            Name = name;    
            Description = description;  
        }
        public string Name {get; set; } 
        public string Description { get; set; } 
    }
}
