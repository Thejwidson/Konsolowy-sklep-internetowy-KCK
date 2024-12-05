using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sklep_Internetowy___Dawid_Szczawiński.Migrations
{
    /// <inheritdoc />
    public partial class testSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "ProductCategoryID", "Name" },
                values: new object[,]
                {
                    { 1, "BLUZA" },
                    { 2, "KOSZULKA" },
                    { 3, "SPODNIE" },
                    { 4, "SPODENKI" },
                    { 5, "CZAPKA" },
                    { 6, "BUTY" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Name", "OrderID", "Price", "ProductCategoryID", "ShoppingCartID" },
                values: new object[,]
                {
                    { 1, "Bluza z kapturem", null, 149.99m, 1, null },
                    { 2, "Bluza sportowa", null, 129.99m, 1, null },
                    { 3, "Bluza polarowa", null, 199.99m, 1, null },
                    { 4, "Koszulka bawełna", null, 49.99m, 2, null },
                    { 5, "Koszulka sportowa", null, 69.99m, 2, null },
                    { 6, "Koszulka polo", null, 89.99m, 2, null },
                    { 7, "Jeansy męskie", null, 199.99m, 3, null },
                    { 8, "Spodnie dresowe", null, 99.99m, 3, null },
                    { 9, "Spodnie materiał", null, 149.99m, 3, null },
                    { 10, "Spodenki jeansowe", null, 79.99m, 4, null },
                    { 11, "Spodenki sportowe", null, 59.99m, 4, null },
                    { 12, "Spodenki plażowe", null, 49.99m, 4, null },
                    { 13, "Czapka z daszkiem", null, 39.99m, 5, null },
                    { 14, "Czapka zimowa", null, 59.99m, 5, null },
                    { 15, "Czapka sportowa", null, 49.99m, 5, null },
                    { 16, "Buty sportowe", null, 299.99m, 6, null },
                    { 17, "Buty trekking", null, 399.99m, 6, null },
                    { 18, "Buty casual", null, 249.99m, 6, null },
                    { 19, "Bluza rozpinana", null, 159.99m, 1, null },
                    { 20, "Koszulka termo", null, 89.99m, 2, null },
                    { 21, "Spodnie bojówki", null, 179.99m, 3, null },
                    { 22, "Spodenki dresowe", null, 69.99m, 4, null },
                    { 23, "Czapka letnia", null, 29.99m, 5, null },
                    { 24, "Buty zimowe", null, 499.99m, 6, null },
                    { 25, "Buty eleganckie", null, 349.99m, 6, null },
                    { 26, "Bluza sportowa z logo", null, 139.99m, 1, null },
                    { 27, "Koszulka oversize", null, 59.99m, 2, null },
                    { 28, "Spodnie slim fit", null, 169.99m, 3, null },
                    { 29, "Spodenki treningowe", null, 79.99m, 4, null },
                    { 30, "Czapka baseball", null, 49.99m, 5, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "ProductCategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "ProductCategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "ProductCategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "ProductCategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "ProductCategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "ProductCategoryID",
                keyValue: 6);
        }
    }
}
