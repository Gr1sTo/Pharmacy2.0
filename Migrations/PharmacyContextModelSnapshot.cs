﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pharmacy;

#nullable disable

namespace Pharmacy.Migrations
{
    [DbContext(typeof(PharmacyContext))]
    partial class PharmacyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Pharmacy.src.Models.Bill", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DateOfBuy")
                        .HasColumnType("datetime2")
                        .HasColumnName("Date_of_buy");

                    b.Property<int?>("DiscountCardID")
                        .HasColumnType("int")
                        .HasColumnName("Discount_card_ID");

                    b.Property<decimal>("FinalPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("final_price");

                    b.Property<int?>("IDPharmacist")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("ID_Pharmacist");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Place");

                    b.Property<string>("TypeOfPay")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Type_of_pay");

                    b.HasKey("ID");

                    b.HasIndex("IDPharmacist");

                    b.ToTable("bill");
                });

            modelBuilder.Entity("Pharmacy.src.Models.GoodsInStorage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Article")
                        .HasColumnType("int")
                        .HasColumnName("article");

                    b.Property<bool>("Availability")
                        .HasColumnType("bit")
                        .HasColumnName("availability");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("expiration_date");

                    b.Property<int>("StorageId")
                        .HasColumnType("int")
                        .HasColumnName("storage_id");

                    b.HasKey("Id");

                    b.HasIndex("Article")
                        .IsUnique();

                    b.HasIndex("StorageId");

                    b.ToTable("goods_in_storage");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Medicine", b =>
                {
                    b.Property<int>("Article")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Article");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Article"));

                    b.Property<string>("ActiveSubstance")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Active_substance");

                    b.Property<int>("CntInPack")
                        .HasColumnType("int")
                        .HasColumnName("cnt_in_pack");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Country");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("Expiration_date");

                    b.Property<string>("MethodOfAdministration")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("method_of_administration");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.Property<string>("Packing")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("packing");

                    b.Property<int?>("ProducerCode")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("producerCode");

                    b.Property<bool>("ReceipeNeed")
                        .HasColumnType("bit")
                        .HasColumnName("receipe_need");

                    b.Property<string>("ReleaseForm")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Release_form");

                    b.Property<int>("Temperature")
                        .HasColumnType("int")
                        .HasColumnName("temperature");

                    b.HasKey("Article");

                    b.HasIndex("ProducerCode");

                    b.ToTable("medicine");
                });

            modelBuilder.Entity("Pharmacy.src.Models.MedicineList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("BillID")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("BillID");

                    b.Property<int>("Count")
                        .HasColumnType("int")
                        .HasColumnName("count_");

                    b.Property<int>("MedArticle")
                        .HasColumnType("int")
                        .HasColumnName("MedArticle");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Price");

                    b.HasKey("ID");

                    b.HasIndex("BillID");

                    b.HasIndex("MedArticle");

                    b.ToTable("medicineList");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Pharmacist", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("SurnameNamePatronymic")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Surname_Name_Patronymic");

                    b.HasKey("ID");

                    b.ToTable("pharmacist");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Producer", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("code");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Code"));

                    b.Property<int>("LicenseNum")
                        .HasColumnType("int")
                        .HasColumnName("licenseNum");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("name");

                    b.HasKey("Code");

                    b.ToTable("producer");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Purchase", b =>
                {
                    b.Property<int>("Id_Purchase")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_Purchase");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Purchase"));

                    b.Property<int>("IDBill")
                        .HasColumnType("int")
                        .HasColumnName("ID_Bill");

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("PhoneNum");

                    b.HasKey("Id_Purchase");

                    b.HasIndex("IDBill");

                    b.HasIndex("PhoneNum");

                    b.ToTable("purchase");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Recipe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("IDBill")
                        .HasColumnType("int")
                        .HasColumnName("ID_bill");

                    b.HasKey("ID");

                    b.HasIndex("IDBill")
                        .IsUnique();

                    b.ToTable("recipe");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Shopper", b =>
                {
                    b.Property<string>("PhoneNum")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("phoneNum");

                    b.Property<float?>("DiscountValue")
                        .IsRequired()
                        .HasColumnType("real")
                        .HasColumnName("discountValue");

                    b.Property<string>("IDDiscountCard")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("ID_discountcard");

                    b.HasKey("PhoneNum");

                    b.ToTable("shopper");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Storage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ShelfCapacity")
                        .HasColumnType("int")
                        .HasColumnName("shelfCapacity");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)")
                        .HasColumnName("type_");

                    b.HasKey("ID");

                    b.ToTable("storage");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Bill", b =>
                {
                    b.HasOne("Pharmacy.src.Models.Pharmacist", "Pharmacist")
                        .WithMany("Bills")
                        .HasForeignKey("IDPharmacist")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pharmacist");
                });

            modelBuilder.Entity("Pharmacy.src.Models.GoodsInStorage", b =>
                {
                    b.HasOne("Pharmacy.src.Models.Medicine", "Medicine")
                        .WithOne("GoodsInStorage")
                        .HasForeignKey("Pharmacy.src.Models.GoodsInStorage", "Article")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pharmacy.src.Models.Storage", "Storage")
                        .WithMany("Goods")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Medicine", b =>
                {
                    b.HasOne("Pharmacy.src.Models.Producer", "Producer")
                        .WithMany("Medicines")
                        .HasForeignKey("ProducerCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("Pharmacy.src.Models.MedicineList", b =>
                {
                    b.HasOne("Pharmacy.src.Models.Bill", "Bill")
                        .WithMany("MedicineLists")
                        .HasForeignKey("BillID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pharmacy.src.Models.Medicine", "Medicine")
                        .WithMany("MedicineLists")
                        .HasForeignKey("MedArticle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Purchase", b =>
                {
                    b.HasOne("Pharmacy.src.Models.Bill", "Bill")
                        .WithMany()
                        .HasForeignKey("IDBill")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pharmacy.src.Models.Shopper", "Shopper")
                        .WithMany("Purchases")
                        .HasForeignKey("PhoneNum")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Shopper");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Recipe", b =>
                {
                    b.HasOne("Pharmacy.src.Models.Bill", "Bill")
                        .WithOne("Recipe")
                        .HasForeignKey("Pharmacy.src.Models.Recipe", "IDBill")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Bill", b =>
                {
                    b.Navigation("MedicineLists");

                    b.Navigation("Recipe")
                        .IsRequired();
                });

            modelBuilder.Entity("Pharmacy.src.Models.Medicine", b =>
                {
                    b.Navigation("GoodsInStorage")
                        .IsRequired();

                    b.Navigation("MedicineLists");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Pharmacist", b =>
                {
                    b.Navigation("Bills");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Producer", b =>
                {
                    b.Navigation("Medicines");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Shopper", b =>
                {
                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("Pharmacy.src.Models.Storage", b =>
                {
                    b.Navigation("Goods");
                });
#pragma warning restore 612, 618
        }
    }
}
