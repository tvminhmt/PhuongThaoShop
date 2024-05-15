using Microsoft.EntityFrameworkCore;
using PTS.Domain.Entities;

namespace PTS.Data
{
    public class ApplicationDbContext : DbContext
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
        // public virtual DbSet<SanPhamGiamGiaEntity> SanPhamGiamGiaEntity { get; set; }
        // public virtual DbSet<GiamGiaEntity> GiamGiaEntity { get; set; }
        public virtual DbSet<UserEntity> UserEntity { get; set; }
        public virtual DbSet<VoucherEntity> VoucherEntity { get; set; }
        public virtual DbSet<ManagePostEntity> ManagePostEntity { get; set; }
        public virtual DbSet<ContactEntity> ContactEntity { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillDetailEntity>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,2)"); // Adjust precision and scale as needed

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
        }

    }
}
