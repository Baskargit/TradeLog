using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShareMarket.TradeLog.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Market",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                        .Annotation("MySql:Collation", "utf8_general_ci"),
                    Code = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                        .Annotation("MySql:Collation", "utf8_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Market", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                        .Annotation("MySql:Collation", "utf8_general_ci"),
                    Code = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                        .Annotation("MySql:Collation", "utf8_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                        .Annotation("MySql:Collation", "utf8_general_ci"),
                    Code = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                        .Annotation("MySql:Collation", "utf8_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                        .Annotation("MySql:Collation", "utf8_general_ci"),
                    Code = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                        .Annotation("MySql:Collation", "utf8_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SymbolType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Market_Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                        .Annotation("MySql:Collation", "utf8_general_ci"),
                    Code = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                        .Annotation("MySql:Collation", "utf8_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Id, x.Market_Id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.UniqueConstraint("AK_SymbolType_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_SymbolType_Market",
                        column: x => x.Market_Id,
                        principalTable: "Market",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Symbol",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SymbolType_Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                        .Annotation("MySql:Collation", "utf8_general_ci"),
                    Code = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                        .Annotation("MySql:Collation", "utf8_general_ci"),
                    Price = table.Column<decimal>(type: "decimal(6,4)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Id, x.SymbolType_Id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.UniqueConstraint("AK_Symbol_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Symbol_SymbolType1",
                        column: x => x.SymbolType_Id,
                        principalTable: "SymbolType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpenTrade",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Symbol_Id = table.Column<int>(nullable: false),
                    TradeType_Id = table.Column<int>(nullable: false),
                    TradeStatus_Id = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(6,4)", nullable: false),
                    Quantity = table.Column<uint>(nullable: false),
                    Time = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Id, x.Symbol_Id, x.TradeType_Id, x.TradeStatus_Id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });
                    table.UniqueConstraint("AK_OpenTrade_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_OpenTrade_Symbol1",
                        column: x => x.Symbol_Id,
                        principalTable: "Symbol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_OpenTrade_TradeStatus1",
                        column: x => x.TradeStatus_Id,
                        principalTable: "TradeStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_OpenTrade_TradeType1",
                        column: x => x.TradeType_Id,
                        principalTable: "TradeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CloseTrade",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OpenTrade_Id = table.Column<long>(nullable: false),
                    TradeType_Id = table.Column<int>(nullable: false),
                    TradeResult_Id = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(6,4)", nullable: false),
                    Quantity = table.Column<uint>(nullable: false),
                    Time = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Id, x.OpenTrade_Id, x.TradeType_Id, x.TradeResult_Id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });
                    table.ForeignKey(
                        name: "fk_CloseTrade_OpenTrade1",
                        column: x => x.OpenTrade_Id,
                        principalTable: "OpenTrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_CloseTrade_TradeResult1",
                        column: x => x.TradeResult_Id,
                        principalTable: "TradeResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_CloseTrade_TradeType1",
                        column: x => x.TradeType_Id,
                        principalTable: "TradeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "fk_CloseTrade_OpenTrade1_idx",
                table: "CloseTrade",
                column: "OpenTrade_Id");

            migrationBuilder.CreateIndex(
                name: "fk_CloseTrade_TradeResult1_idx",
                table: "CloseTrade",
                column: "TradeResult_Id");

            migrationBuilder.CreateIndex(
                name: "fk_CloseTrade_TradeType1_idx",
                table: "CloseTrade",
                column: "TradeType_Id");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE",
                table: "OpenTrade",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_OpenTrade_Symbol1_idx",
                table: "OpenTrade",
                column: "Symbol_Id");

            migrationBuilder.CreateIndex(
                name: "fk_OpenTrade_TradeStatus1_idx",
                table: "OpenTrade",
                column: "TradeStatus_Id");

            migrationBuilder.CreateIndex(
                name: "fk_OpenTrade_TradeType1_idx",
                table: "OpenTrade",
                column: "TradeType_Id");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE",
                table: "Symbol",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_Symbol_SymbolType1_idx",
                table: "Symbol",
                column: "SymbolType_Id");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE",
                table: "SymbolType",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_SymbolType_Market_idx",
                table: "SymbolType",
                column: "Market_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CloseTrade");

            migrationBuilder.DropTable(
                name: "OpenTrade");

            migrationBuilder.DropTable(
                name: "TradeResult");

            migrationBuilder.DropTable(
                name: "Symbol");

            migrationBuilder.DropTable(
                name: "TradeStatus");

            migrationBuilder.DropTable(
                name: "TradeType");

            migrationBuilder.DropTable(
                name: "SymbolType");

            migrationBuilder.DropTable(
                name: "Market");
        }
    }
}
