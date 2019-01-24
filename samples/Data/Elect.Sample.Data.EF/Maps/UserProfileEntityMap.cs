using Elect.Data.EF.Services.Map;
using Elect.Sample.Data.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Elect.Sample.Data.EF.Maps
{
    public class UserProfileEntityMap : EntityTypeConfiguration<UserProfileEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<UserProfileEntity> builder)
        {
            base.Map(builder);

            builder.ToTable(nameof(UserProfileEntity));

            builder.HasOne(x => x.User).WithMany(x => x.Profiles).HasForeignKey(x => x.UserId);
        }
    }
}