using F.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F.API.Data.Mappings;

public class PlayerMapping : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.HasMany(c => c.Ranks)
            .WithOne(c => c.Player)
            .HasForeignKey(c => c.PlayerId);

        builder.ToTable("Player");
    }
}