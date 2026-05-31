using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SatMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusLeitura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LimiteMax",
                table: "SENSORES",
                type: "BINARY_DOUBLE",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LimiteMin",
                table: "SENSORES",
                type: "BINARY_DOUBLE",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LEITURAS_SENSOR",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimiteMax",
                table: "SENSORES");

            migrationBuilder.DropColumn(
                name: "LimiteMin",
                table: "SENSORES");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LEITURAS_SENSOR");
        }
    }
}
