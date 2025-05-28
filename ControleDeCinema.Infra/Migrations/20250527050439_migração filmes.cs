using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeCinema.Infra.Migrations
{
    /// <inheritdoc />
    public partial class migraçãofilmes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_CATEGORIA_FILME_Filme_FilmesId",
                table: "TB_CATEGORIA_FILME");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Filme",
                table: "Filme");

            migrationBuilder.RenameTable(
                name: "Filme",
                newName: "TB_FILME");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "TB_FILME",
                type: "varchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_FILME",
                table: "TB_FILME",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_CATEGORIA_FILME_TB_FILME_FilmesId",
                table: "TB_CATEGORIA_FILME",
                column: "FilmesId",
                principalTable: "TB_FILME",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_CATEGORIA_FILME_TB_FILME_FilmesId",
                table: "TB_CATEGORIA_FILME");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_FILME",
                table: "TB_FILME");

            migrationBuilder.RenameTable(
                name: "TB_FILME",
                newName: "Filme");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Filme",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Filme",
                table: "Filme",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_CATEGORIA_FILME_Filme_FilmesId",
                table: "TB_CATEGORIA_FILME",
                column: "FilmesId",
                principalTable: "Filme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
