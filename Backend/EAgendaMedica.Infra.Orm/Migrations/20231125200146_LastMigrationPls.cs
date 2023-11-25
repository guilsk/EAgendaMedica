using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EAgendaMedica.Infra.Orm.Migrations
{
    /// <inheritdoc />
    public partial class LastMigrationPls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TBMedico",
                columns: new[] { "Id", "Crm", "Nome" },
                values: new object[,]
                {
                    { new Guid("3644dbf6-8539-48ea-80d0-4f346f7f586f"), "12345-AZ", "Dr. Teste III" },
                    { new Guid("6b5082c9-f463-426a-a299-7562497688e9"), "09876-ZZ", "Dr. Teste II" },
                    { new Guid("c67354d9-a94a-4342-a3ff-c71de8cb6883"), "00000-AA", "Dr. Teste I" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TBMedico",
                keyColumn: "Id",
                keyValue: new Guid("3644dbf6-8539-48ea-80d0-4f346f7f586f"));

            migrationBuilder.DeleteData(
                table: "TBMedico",
                keyColumn: "Id",
                keyValue: new Guid("6b5082c9-f463-426a-a299-7562497688e9"));

            migrationBuilder.DeleteData(
                table: "TBMedico",
                keyColumn: "Id",
                keyValue: new Guid("c67354d9-a94a-4342-a3ff-c71de8cb6883"));
        }
    }
}
