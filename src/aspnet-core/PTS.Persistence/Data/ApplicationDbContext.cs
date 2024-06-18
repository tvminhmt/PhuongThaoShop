using App.Shared.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NAudio.CoreAudioApi;
using PTS.Domain.Entities;

namespace PTS.Data
{
    public class ApplicationDbContext :IdentityDbContext<UserEntity, RoleEntity, int>
	{
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        public virtual DbSet<RamEntity> RamEntity { get; set; }
        public virtual DbSet<CpuEntity> CpuEntity { get; set; }
        public virtual DbSet<ColorEntity> ColorEntity { get; set; }
        public virtual DbSet<HardDriveEntity> HardDriveEntity { get; set; }
        public virtual DbSet<ScreenEntity> ScreenEntity { get; set; }
        public virtual DbSet<CardVGAEntity> CardVGAEntity { get; set; }
        public virtual DbSet<BillEntity> BillEntity { get; set; }
        public virtual DbSet<BillDetailEntity> BillDetailEntity { get; set; }
        public virtual DbSet<CartEntity> CartEntity { get; set; }
        public virtual DbSet<CartDetailEntity> CartDetailEntity { get; set; }
        public virtual DbSet<ImageEntity> ImageEntity { get; set; }
        public virtual DbSet<SerialEntity> SerialEntity { get; set; }
        public virtual DbSet<ManufacturerEntity> ManufacturerEntity { get; set; }
        public virtual DbSet<ProductEntity> ProductEntity { get; set; }
        public virtual DbSet<ProductTypeEntity> ProductTypeEntity { get; set; }
        public virtual DbSet<ProductDetailEntity> ProductDetailEntity { get; set; }
        public virtual DbSet<RoleEntity> RoleEntity { get; set; }
        public virtual DbSet<UserEntity> UserEntity { get; set; }
        public virtual DbSet<VoucherEntity> VoucherEntity { get; set; }
		public virtual DbSet<SeoEntity> SeoEntity { get; set; }
		public virtual DbSet<ManagePostEntity> ManagePostEntity { get; set; }
        public virtual DbSet<ContactEntity> ContactEntity { get; set; }

	
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				var tableName = entityType.GetTableName();
				if (tableName.StartsWith("AspNet"))
				{
					entityType.SetTableName(tableName.Substring(6));
				}
			}
			modelBuilder.Entity<BillDetailEntity>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,2)"); 

            modelBuilder.Entity<BillEntity>()
                .Property(e => e.Discount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ProductDetailEntity>()
                .Property(e => e.OldPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ProductDetailEntity>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<VoucherEntity>()
                .Property(e => e.GiaTri)
                .HasColumnType("decimal(18,2)");

			SeedUsers(modelBuilder);
			SeedRoles(modelBuilder);
			SeedUserRoles(modelBuilder);

		}
		private void SeedUsers(ModelBuilder builder)
		{
			UserEntity user = new UserEntity()
			{
				Id = 1,
				UserName = "adminphuongthao",
				Email = "admin@phuongthaoshop.vn",
				FullName = "Administrator",
				NormalizedUserName = "ADMIN",
				NormalizedEmail = "ADMIN@PHUONGTHAOSHOP.VN",
				SecurityStamp = "phuongthaoshop.vn",
				ConcurrencyStamp = "phuongthaoshop.vn"
			};

			PasswordHasher<UserEntity> passwordHasher = new PasswordHasher<UserEntity>();
			user.PasswordHash = passwordHasher.HashPassword(user, "adminphuongthao");

			builder.Entity<UserEntity>().HasData(user);
		}

		private void SeedRoles(ModelBuilder builder)
		{
			builder.Entity<RoleEntity>().HasData(
				new RoleEntity() { Id = 1, Name = "Admin", NormalizedName = "ADMIN", Description = "Administrator", ConcurrencyStamp = "1" },
				new RoleEntity() { Id = 2, Name = "Employee", NormalizedName = "EMPLOYEE", Description = "Employee", ConcurrencyStamp = "1" },
				new RoleEntity() { Id = 3, Name = "Customer", NormalizedName = "CUSTOMER", Description = "Customer", ConcurrencyStamp = "1" }
			);
		}

		private void SeedUserRoles(ModelBuilder builder)
		{
			builder.Entity<IdentityUserRole<int>>().HasData(
				new IdentityUserRole<int>() { RoleId = 1, UserId = 1 }
			);
		}

	}
}
