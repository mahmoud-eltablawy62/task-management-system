using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;

namespace TaskManagementSystem.Repository.Data.Config
{
    public class Comment_Config : IEntityTypeConfiguration <Comment>
    {
       

        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(P => P.Taskats)
             .WithMany()
             .HasForeignKey(P => P.Taskats_Id);
        }
    }
}
