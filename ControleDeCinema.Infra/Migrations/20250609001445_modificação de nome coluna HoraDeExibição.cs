using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeCinema.Infra.Migrations
{
    /// <inheritdoc />
    public partial class modificaçãodenomecolunaHoraDeExibição : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_FILME_TB_CATEGORIA_CategoriaId1",
                table: "TB_FILME");

            migrationBuilder.DropIndex(
                name: "IX_TB_FILME_CategoriaId1",
                table: "TB_FILME");

            migrationBuilder.DropColumn(
                name: "CategoriaId1",
                table: "TB_FILME");

            migrationBuilder.RenameColumn(
                name: "HoraDeExibicao",
                table: "TB_SESSAO",
                newName: "HorarioDaSessao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HorarioDaSessao",
                table: "TB_SESSAO",
                newName: "HoraDeExibicao");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId1",
                table: "TB_FILME",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_FILME_CategoriaId1",
                table: "TB_FILME",
                column: "CategoriaId1",
                unique: true,
                filter: "[CategoriaId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_FILME_TB_CATEGORIA_CategoriaId1",
                table: "TB_FILME",
                column: "CategoriaId1",
                principalTable: "TB_CATEGORIA",
                principalColumn: "Id");
        }
    }
}
