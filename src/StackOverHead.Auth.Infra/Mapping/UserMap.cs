using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackOverHead.Auth.Domain.Entities;
using StackOverHead.Auth.Domain.ValueObjects;
using StackOverHead.Auth.Infra.Models;

namespace StackOverHead.Auth.Infra.Mapping
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("USER");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
               .HasIndex(u => u.Email)
               .IsUnique();

            builder
                .Property(u => u.Hash)
                .IsRequired();

            builder
                .Property(u => u.Salt)
                .IsRequired();

            var userTable = GetUserTable();
            builder.HasData(userTable);
        }

        public UserModel GetUserTable()
        {
            var pass = new Password("admin");
            var user = new User("admin", "admin@admin.com", pass, "ADMIN");
            var userTable = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Email = user.Email,
                Hash = user.Password.Hash,
                Salt = user.Password.Salt,
                Roles = user.Roles
            };
            return userTable;
        }
    }
}
