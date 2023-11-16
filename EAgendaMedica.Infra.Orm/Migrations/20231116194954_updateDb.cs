using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAgendaMedica.Infra.Orm.Migrations
{
    /// <inheritdoc />
    public partial class updateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AtividadeMedico_TBAtividade_AtividadesId",
                table: "AtividadeMedico");

            migrationBuilder.DropForeignKey(
                name: "FK_AtividadeMedico_TBMedico_MedicosId",
                table: "AtividadeMedico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AtividadeMedico",
                table: "AtividadeMedico");

            migrationBuilder.RenameTable(
                name: "AtividadeMedico",
                newName: "TBMedico_TBAtividade");

            migrationBuilder.RenameIndex(
                name: "IX_AtividadeMedico_MedicosId",
                table: "TBMedico_TBAtividade",
                newName: "IX_TBMedico_TBAtividade_MedicosId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBMedico_TBAtividade",
                table: "TBMedico_TBAtividade",
                columns: new[] { "AtividadesId", "MedicosId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TBMedico_TBAtividade_TBAtividade_AtividadesId",
                table: "TBMedico_TBAtividade",
                column: "AtividadesId",
                principalTable: "TBAtividade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBMedico_TBAtividade_TBMedico_MedicosId",
                table: "TBMedico_TBAtividade",
                column: "MedicosId",
                principalTable: "TBMedico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBMedico_TBAtividade_TBAtividade_AtividadesId",
                table: "TBMedico_TBAtividade");

            migrationBuilder.DropForeignKey(
                name: "FK_TBMedico_TBAtividade_TBMedico_MedicosId",
                table: "TBMedico_TBAtividade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBMedico_TBAtividade",
                table: "TBMedico_TBAtividade");

            migrationBuilder.RenameTable(
                name: "TBMedico_TBAtividade",
                newName: "AtividadeMedico");

            migrationBuilder.RenameIndex(
                name: "IX_TBMedico_TBAtividade_MedicosId",
                table: "AtividadeMedico",
                newName: "IX_AtividadeMedico_MedicosId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AtividadeMedico",
                table: "AtividadeMedico",
                columns: new[] { "AtividadesId", "MedicosId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AtividadeMedico_TBAtividade_AtividadesId",
                table: "AtividadeMedico",
                column: "AtividadesId",
                principalTable: "TBAtividade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AtividadeMedico_TBMedico_MedicosId",
                table: "AtividadeMedico",
                column: "MedicosId",
                principalTable: "TBMedico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
