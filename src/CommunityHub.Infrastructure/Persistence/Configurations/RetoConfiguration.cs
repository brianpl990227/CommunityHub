using CommunityHub.Domain.Retos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CommunityHub.Infrastructure.Persistence.Configurations;

internal sealed class RetoConfiguration : IEntityTypeConfiguration<Reto>
{
    public void Configure(EntityTypeBuilder<Reto> builder)
    {
        builder.ToTable("Retos");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Titulo)
            .HasMaxLength(Reto.LongitudMaximaTitulo)
            .IsRequired();

        builder.Property(r => r.Enunciado).IsRequired();

        // SQLite no sabe ordenar ni comparar DateTimeOffset: se guarda como número
        // (https://learn.microsoft.com/ef/core/providers/sqlite/limitations).
        builder.Property(r => r.PublicadoEl)
            .HasConversion(new DateTimeOffsetToBinaryConverter());
    }
}
