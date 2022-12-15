using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PYPJ.Models;

public partial class PypjContext : DbContext
{
    public PypjContext()
    {
    }

    public PypjContext(DbContextOptions<PypjContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblMenu> TblMenus { get; set; }

    public virtual DbSet<TblProgram> TblPrograms { get; set; }

    public virtual DbSet<TblRefreshToken> TblRefreshTokens { get; set; }

    public virtual DbSet<TblRoleMenuPermission> TblRoleMenuPermissions { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblUserRole> TblUserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=PYPJ;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblMenu>(entity =>
        {
            entity.ToTable("tbl_Menu");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.MenuName).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ParentMenuId).HasColumnName("ParentMenuID");
            entity.Property(e => e.ProgramId).HasColumnName("ProgramID");
        });

        modelBuilder.Entity<TblProgram>(entity =>
        {
            entity.ToTable("tbl_Program");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ParentProgramId).HasColumnName("ParentProgramID");
            entity.Property(e => e.ProgramUrl).HasMaxLength(50);
        });

        modelBuilder.Entity<TblRefreshToken>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("tbl_RefreshToken");

            entity.Property(e => e.UserId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblRoleMenuPermission>(entity =>
        {
            entity.ToTable("tbl_RoleMenuPermission");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.MenuId).HasColumnName("MenuID");
            entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("tbl_User");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        modelBuilder.Entity<TblUserRole>(entity =>
        {
            entity.ToTable("tbl_UserRole");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
