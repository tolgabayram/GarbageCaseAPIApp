using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarbageCaseAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WasteTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WasteTypeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    WasteProperty = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WasteRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    WasteTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ReceivingCompany = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    WasteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WasteExplanation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WasteRecords_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WasteRecords_WasteTypes_WasteTypeId",
                        column: x => x.WasteTypeId,
                        principalTable: "WasteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WasteRecords_StoreId",
                table: "WasteRecords",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteRecords_WasteTypeId",
                table: "WasteRecords",
                column: "WasteTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WasteRecords");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "WasteTypes");
        }
    }
}
