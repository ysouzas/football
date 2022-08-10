using F.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F.API.Data.Mappings;

public class RankMapping : IEntityTypeConfiguration<Rank>
{
    public void Configure(EntityTypeBuilder<Rank> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Player)
            .WithMany(c => c.Ranks);

        builder.ToTable("Rank");
    }
}