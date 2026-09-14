using EddActividad1.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EddActividad1.Data;

public class MicroAppDBcontext : DbContext
{
    // si llego a ponerle una DB
    public MicroAppDBcontext(DbContextOptions<MicroAppDBcontext> options) : base(options)
    {
        
    }
    
    public DbSet<Micro> Micros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Micro>(tb =>
        {
            tb.HasKey(col => col.Placa);
            tb.Property(col => col.Linea).HasMaxLength(50);
            tb.ToTable("Micro");
        });
    }
}
