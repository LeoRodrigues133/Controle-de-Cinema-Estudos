using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeCinema.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Corrigindo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CATEGORIA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CATEGORIA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_SALAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacidade = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SALAS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_FILME",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    Duracao = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FILME", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_FILME_TB_CATEGORIA_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "TB_CATEGORIA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_FILME_TB_CATEGORIA_CategoriaId1",
                        column: x => x.CategoriaId1,
                        principalTable: "TB_CATEGORIA",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_SESSAO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmeId = table.Column<int>(type: "int", nullable: false),
                    SalaId = table.Column<int>(type: "int", nullable: false),
                    Finalizada = table.Column<bool>(type: "bit", nullable: false),
                    DataDeExibicao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraDeExibicao = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SESSAO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_SESSAO_TB_FILME_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "TB_FILME",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_SESSAO_TB_SALAS_SalaId",
                        column: x => x.SalaId,
                        principalTable: "TB_SALAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ASSENTOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    SessaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ASSENTOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ASSENTOS_TB_SESSAO_SessaoId",
                        column: x => x.SessaoId,
                        principalTable: "TB_SESSAO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ASSENTOS_SessaoId",
                table: "TB_ASSENTOS",
                column: "SessaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_FILME_CategoriaId",
                table: "TB_FILME",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_FILME_CategoriaId1",
                table: "TB_FILME",
                column: "CategoriaId1",
                unique: true,
                filter: "[CategoriaId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SESSAO_FilmeId",
                table: "TB_SESSAO",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SESSAO_SalaId",
                table: "TB_SESSAO",
                column: "SalaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ASSENTOS");

            migrationBuilder.DropTable(
                name: "TB_SESSAO");

            migrationBuilder.DropTable(
                name: "TB_FILME");

            migrationBuilder.DropTable(
                name: "TB_SALAS");

            migrationBuilder.DropTable(
                name: "TB_CATEGORIA");
        }
    }
}
