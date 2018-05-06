using Elect.Data.EF.Services.Map;
using Elect.Sample.Data.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Elect.Sample.Data.EF.Maps
{
    public class UserEntityMap : EntityTypeConfiguration<UserEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<UserEntity> builder)
        {
            base.Map(builder);

            builder.ToTable(nameof(UserEntity));

            builder.HasIndex(x => x.UserName);
        }
    }
}