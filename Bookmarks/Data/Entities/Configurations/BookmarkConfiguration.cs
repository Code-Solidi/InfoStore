using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.Security.Claims;

namespace Bookmarks.Data.Entities.Configurations
{
    public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
    {
        // https://mywebshosting.com/what-is-the-maximum-url-length-limit-in-browsers/#How_long_should_a_URL_be_Or_How_long_can_a_URL_be
        const int MaxUrlLength = 2083;
        const int MaxTitleLength = 80;

        public BookmarkConfiguration()   // it is null during migrations
        {
        }

        public void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            builder.ToTable("Bookmarks", "Info");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.UserId).IsRequired().HasMaxLength(450); // Id length in Identity

            builder.Property(x => x.Url).IsRequired().HasMaxLength(MaxUrlLength);
            builder.HasIndex(x => x.Url).IsUnique();

            builder.Property(x => x.UserId).IsRequired().HasMaxLength(450); // Id length in Identity
            builder.Property(x => x.Title).HasMaxLength(MaxTitleLength);

            builder.HasMany(x => x.Tags).WithMany(x => x.Bookmarks);
        }
    }
}