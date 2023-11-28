using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pharmacist",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname_Name_Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pharmacist", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "producer",
                columns: table => new
                {
                    code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    licenseNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producer", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "shopper",
                columns: table => new
                {
                    phoneNum = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    discountValue = table.Column<float>(type: "real", nullable: false),
                    ID_discountcard = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shopper", x => x.phoneNum);
                });

            migrationBuilder.CreateTable(
                name: "storage",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type_ = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    shelfCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storage", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "bill",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Pharmacist = table.Column<int>(type: "int", nullable: false),
                    Discount_card_ID = table.Column<int>(type: "int", nullable: true),
                    final_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type_of_pay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Date_of_buy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bill", x => x.ID);
                    table.ForeignKey(
                        name: "FK_bill_pharmacist_ID_Pharmacist",
                        column: x => x.ID_Pharmacist,
                        principalTable: "pharmacist",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medicine",
                columns: table => new
                {
                    Article = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active_substance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Expiration_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    method_of_administration = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Release_form = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cnt_in_pack = table.Column<int>(type: "int", nullable: false),
                    receipe_need = table.Column<bool>(type: "bit", nullable: false),
                    temperature = table.Column<int>(type: "int", nullable: false),
                    packing = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    producerCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicine", x => x.Article);
                    table.ForeignKey(
                        name: "FK_medicine_producer_producerCode",
                        column: x => x.producerCode,
                        principalTable: "producer",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase",
                columns: table => new
                {
                    Id_Purchase = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNum = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    ID_Bill = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase", x => x.Id_Purchase);
                    table.ForeignKey(
                        name: "FK_purchase_bill_ID_Bill",
                        column: x => x.ID_Bill,
                        principalTable: "bill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_shopper_PhoneNum",
                        column: x => x.PhoneNum,
                        principalTable: "shopper",
                        principalColumn: "phoneNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recipe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_bill = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_recipe_bill_ID_bill",
                        column: x => x.ID_bill,
                        principalTable: "bill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "goods_in_storage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    article = table.Column<int>(type: "int", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    availability = table.Column<bool>(type: "bit", nullable: false),
                    storage_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goods_in_storage", x => x.id);
                    table.ForeignKey(
                        name: "FK_goods_in_storage_medicine_article",
                        column: x => x.article,
                        principalTable: "medicine",
                        principalColumn: "Article",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_goods_in_storage_storage_storage_id",
                        column: x => x.storage_id,
                        principalTable: "storage",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medicineList",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedArticle = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    count_ = table.Column<int>(type: "int", nullable: false),
                    BillID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicineList", x => x.ID);
                    table.ForeignKey(
                        name: "FK_medicineList_bill_BillID",
                        column: x => x.BillID,
                        principalTable: "bill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_medicineList_medicine_MedArticle",
                        column: x => x.MedArticle,
                        principalTable: "medicine",
                        principalColumn: "Article",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bill_ID_Pharmacist",
                table: "bill",
                column: "ID_Pharmacist");

            migrationBuilder.CreateIndex(
                name: "IX_goods_in_storage_article",
                table: "goods_in_storage",
                column: "article",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_goods_in_storage_storage_id",
                table: "goods_in_storage",
                column: "storage_id");

            migrationBuilder.CreateIndex(
                name: "IX_medicine_producerCode",
                table: "medicine",
                column: "producerCode");

            migrationBuilder.CreateIndex(
                name: "IX_medicineList_BillID",
                table: "medicineList",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_medicineList_MedArticle",
                table: "medicineList",
                column: "MedArticle");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_ID_Bill",
                table: "purchase",
                column: "ID_Bill");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_PhoneNum",
                table: "purchase",
                column: "PhoneNum");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_ID_bill",
                table: "recipe",
                column: "ID_bill",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "goods_in_storage");

            migrationBuilder.DropTable(
                name: "medicineList");

            migrationBuilder.DropTable(
                name: "purchase");

            migrationBuilder.DropTable(
                name: "recipe");

            migrationBuilder.DropTable(
                name: "storage");

            migrationBuilder.DropTable(
                name: "medicine");

            migrationBuilder.DropTable(
                name: "shopper");

            migrationBuilder.DropTable(
                name: "bill");

            migrationBuilder.DropTable(
                name: "producer");

            migrationBuilder.DropTable(
                name: "pharmacist");
        }
    }
}
