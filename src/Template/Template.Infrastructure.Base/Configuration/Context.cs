using Microsoft.EntityFrameworkCore;
using Template.Domain.Core.Entities;
using Template.Infrastructure.Base.Mappings;

namespace Template.Infrastructure.Base.Configuration;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityBaseConfiguration).Assembly);
    }
}