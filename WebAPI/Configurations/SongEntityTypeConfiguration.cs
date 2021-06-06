using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Shared.Models;

namespace MyApp.WebAPI.Configurations

{
    class SongEntityTypeConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Singer).IsRequired();
        }
    }
}
