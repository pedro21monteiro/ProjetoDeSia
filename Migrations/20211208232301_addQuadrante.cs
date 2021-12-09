using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoDeSia.Migrations
{
    public partial class addQuadrante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quadrante",
                columns: table => new
                {
                    IdQuadrante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Quad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosicaoQuadrante = table.Column<int>(type: "int", nullable: false),
                    TecnicaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quadrante", x => x.IdQuadrante);
                    table.ForeignKey(
                        name: "FK_Quadrante_Tecnica_TecnicaId",
                        column: x => x.TecnicaId,
                        principalTable: "Tecnica",
                        principalColumn: "IdTecnica",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quadrante_TecnicaId",
                table: "Quadrante",
                column: "TecnicaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quadrante");
        }
    }
}
