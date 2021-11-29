using Microsoft.EntityFrameworkCore;
using WSI.KommentarService.Models;

namespace WSI.KommentarService.Data;

public class DataContext : DbContext {
    public DataContext(DbContextOptions options) : base(options) { }

    public DbSet<KommentarModel> Kommentare { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<KommentarModel>()
            .HasKey(k => k.Id);

        modelBuilder.Entity<KommentarModel>()
            .HasIndex(i => i.KopfId);

        modelBuilder.Entity<KommentarModel>()
            .HasOne(k => k.KopfKommentar)
            .WithMany(k => k.AlleKommentare)
            .HasForeignKey(k => k.KopfId)
            .HasPrincipalKey(k => k.Id)
            .IsRequired(false);

        modelBuilder.Entity<KommentarModel>()
            .HasOne(k => k.ÜbergeordneterKommentar)
            .WithMany(k => k.WeitereKommentare)
            .HasForeignKey(k => k.ÜbergeordneterKommentarId)
            .HasPrincipalKey(k => k.Id)
            .IsRequired(false);
    }
}
