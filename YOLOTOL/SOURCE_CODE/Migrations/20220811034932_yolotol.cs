using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOLOTOL.Migrations
{
    public partial class yolotol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(nullable: true),
                    apellidoP = table.Column<string>(nullable: true),
                    apellidoM = table.Column<string>(nullable: true),
                    correo = table.Column<string>(nullable: true),
                    contrasenia = table.Column<string>(nullable: true),
                    fechaNacimiento = table.Column<DateTime>(nullable: false),
                    fotoPerfil = table.Column<string>(nullable: true),
                    cedula = table.Column<string>(nullable: true),
                    telefono = table.Column<string>(nullable: true),
                    tipoUsuario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    IdProducto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 25, nullable: false),
                    Imagen = table.Column<string>(nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(maxLength: 50, nullable: false),
                    IdCategoria = table.Column<int>(nullable: false),
                    categoriaIdCategoria = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_Producto_Categoria_categoriaIdCategoria",
                        column: x => x.categoriaIdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Producto_categoriaIdCategoria",
                table: "Producto",
                column: "categoriaIdCategoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
