using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListing.Migrations
{
    public partial class initcong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "26f94aef-d560-402a-b83a-8cd519141f33", "86177e26-bd26-431b-8b95-6f7a0e2d7532", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "652bc2a3-c14d-49fc-a9ef-ce8b5fc98c08", "8538f863-c5a7-48d3-a31b-7c41a88ca348", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6f21ec94-397e-4a75-8cb9-e0eea4dba3f3", "cb5d39f4-65c2-4917-b51f-235bf34bbc74", "Adminisstrator", "ADMINISSTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26f94aef-d560-402a-b83a-8cd519141f33");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "652bc2a3-c14d-49fc-a9ef-ce8b5fc98c08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f21ec94-397e-4a75-8cb9-e0eea4dba3f3");
        }
    }
}
