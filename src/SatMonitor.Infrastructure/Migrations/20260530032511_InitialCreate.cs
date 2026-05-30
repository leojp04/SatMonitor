using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SatMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MISSOES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MISSOES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SATELITES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Altitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Inclinacao = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    MissaoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SATELITES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SATELITES_MISSOES_MissaoId",
                        column: x => x.MissaoId,
                        principalTable: "MISSOES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SENSORES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Unidade = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    SateliteId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SENSORES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SENSORES_SATELITES_SateliteId",
                        column: x => x.SateliteId,
                        principalTable: "SATELITES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LEITURAS_SENSOR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Valor = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    DataHoraLeitura = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    SensorId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEITURAS_SENSOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LEITURAS_SENSOR_SENSORES_SensorId",
                        column: x => x.SensorId,
                        principalTable: "SENSORES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LEITURAS_SENSOR_SensorId",
                table: "LEITURAS_SENSOR",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_SATELITES_MissaoId",
                table: "SATELITES",
                column: "MissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_SENSORES_SateliteId",
                table: "SENSORES",
                column: "SateliteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LEITURAS_SENSOR");

            migrationBuilder.DropTable(
                name: "SENSORES");

            migrationBuilder.DropTable(
                name: "SATELITES");

            migrationBuilder.DropTable(
                name: "MISSOES");
        }
    }
}
