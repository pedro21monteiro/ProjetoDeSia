using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoDeSia.Migrations
{
    public partial class addItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    IdItem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pontucao = table.Column<double>(type: "float", nullable: false),
                    Importancia = table.Column<int>(type: "int", nullable: false),
                    classificacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TecnicaId = table.Column<int>(type: "int", nullable: false),
                    PosicaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.IdItem);
                    table.ForeignKey(
                        name: "FK_Item_Tecnica_TecnicaId",
                        column: x => x.TecnicaId,
                        principalTable: "Tecnica",
                        principalColumn: "IdTecnica",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_TecnicaId",
                table: "Item",
                column: "TecnicaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}
