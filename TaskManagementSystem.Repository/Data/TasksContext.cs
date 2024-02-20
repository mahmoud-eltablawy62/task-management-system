﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;

namespace Talabat.Repository.Data
{
    public class TasksContext : DbContext
    {
        public TasksContext(DbContextOptions<TasksContext>  options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Taskat> Products { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
