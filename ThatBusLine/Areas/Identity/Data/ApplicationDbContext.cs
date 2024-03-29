﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThatBusLine.Models;

namespace ThatBusLine.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfig());
    }

    public DbSet<ThatBusLine.Models.Announcement>? Announcement { get; set; }

    public DbSet<ThatBusLine.Models.Ticket>? Ticket { get; set; }

    public DbSet<ThatBusLine.Models.ProjectRole>? ProjectRole { get; set; }
}

public class ApplicationUserEntityConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(n => n.FirstName).HasMaxLength(25);
        builder.Property(n => n.LastName).HasMaxLength(25);
        builder.Property(n => n.Location).HasMaxLength(25);
        builder.Property(n => n.Credit).HasMaxLength(25);
    }
}