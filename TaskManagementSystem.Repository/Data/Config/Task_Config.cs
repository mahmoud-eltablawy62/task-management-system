using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.core.Entities;

namespace TaskManagementSystem.Repository.Data.Config
{
    internal class Task_Config : IEntityTypeConfiguration<Taskat>
    {
        public void Configure(EntityTypeBuilder<Taskat> builder)
        {
            builder.HasOne(P => P.Category)
                .WithMany()
                .HasForeignKey(P => P.CategoryId);  
            
        }
    }
}
