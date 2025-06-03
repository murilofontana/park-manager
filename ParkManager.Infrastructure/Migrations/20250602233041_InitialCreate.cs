using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Establishments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Cnpj = table.Column<string>(type: "TEXT", maxLength: 14, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    Street = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Complement = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    MotorcyclesParkingSpaces = table.Column<int>(type: "INTEGER", nullable: false),
                    CarsParkingSpaces = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Establishments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Branch = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Color = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Plate = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingMovements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParkingId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VehicleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExitDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EstablishmentId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingMovements_Establishments_EstablishmentId",
                        column: x => x.EstablishmentId,
                        principalTable: "Establishments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingMovements_EstablishmentId",
                table: "ParkingMovements",
                column: "EstablishmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingMovements");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Establishments");
        }
    }
}
