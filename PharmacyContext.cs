using Microsoft.EntityFrameworkCore;
using Pharmacy.src.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pharmacy
{
    public class PharmacyContext : DbContext
    {
        public PharmacyContext() 
        {

        }

        public PharmacyContext(DbContextOptions<PharmacyContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-EUJTV8V;Database=Pharmacy3;Integrated Security = true; TrustServerCertificate=true;");
        }

        public DbSet<Shopper> Shoppers { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<MedicineList> MedicineLists { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<GoodsInStorage> goodsInStorages { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Storage> Storages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Shopper з Purchase (Один-до-багатьох)
            modelBuilder.Entity<Shopper>()
                .HasMany(s => s.Purchases)
                .WithOne(p => p.Shopper)
                .HasForeignKey(p => p.PhoneNum);

            // Pharmacist з Bill (Один-до-багатьох)
            modelBuilder.Entity<Pharmacist>()
                .HasMany(p => p.Bills)
                .WithOne(b => b.Pharmacist)
                .HasForeignKey(b => b.IDPharmacist);

            // Storage з GoodsInStorage (Один-до-багатьох)
            modelBuilder.Entity<Storage>()
                .HasMany(s => s.Goods)
                .WithOne(g => g.Storage)
                .HasForeignKey(g => g.StorageId);

            // Producer з Medicine (Один-до-багатьох)
            modelBuilder.Entity<Producer>()
                .HasMany(p => p.Medicines)
                .WithOne(m => m.Producer)
                .HasForeignKey(m => m.ProducerCode);

            // Medicine з MedicineList (Один-до-багатьох)
            modelBuilder.Entity<Medicine>()
                .HasMany(m => m.MedicineLists)
                .WithOne(ml => ml.Medicine)
                .HasForeignKey(ml => ml.MedArticle);

            // Medicine з GoodsInStorage (Один-до-одного)
            modelBuilder.Entity<Medicine>()
                .HasOne(m => m.GoodsInStorage)
                .WithOne(g => g.Medicine)
                .HasForeignKey<GoodsInStorage>(g => g.Article);

            // Bill з MedicineList (Один-до-багатьох)
            modelBuilder.Entity<Bill>()
                .HasMany(b => b.MedicineLists)
                .WithOne(ml => ml.Bill)
                .HasForeignKey(ml => ml.BillID);

            // Bill з Recipe (Один-до-одного)
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Recipe)
                .WithOne(r => r.Bill)
                .HasForeignKey<Recipe>(r => r.IDBill);

            base.OnModelCreating(modelBuilder);
        }

    }

}
