using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBackend.Migrations
{
    /// <inheritdoc />
    public partial class NormalizacionCatalogos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Crear tablas catálogo
            migrationBuilder.CreateTable(name: "CondicionEjemplares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false).Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                }, constraints: table => { table.PrimaryKey("PK_CondicionEjemplares", x => x.Id); });
            migrationBuilder.CreateTable(name: "EstadoEjemplares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false).Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                }, constraints: table => { table.PrimaryKey("PK_EstadoEjemplares", x => x.Id); });
            migrationBuilder.CreateTable(name: "EstadoPrestamos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false).Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                }, constraints: table => { table.PrimaryKey("PK_EstadoPrestamos", x => x.Id); });
            migrationBuilder.CreateTable(name: "EstadoReservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false).Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                }, constraints: table => { table.PrimaryKey("PK_EstadoReservas", x => x.Id); });
            migrationBuilder.CreateTable(name: "EstadoVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false).Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                }, constraints: table => { table.PrimaryKey("PK_EstadoVentas", x => x.Id); });
            migrationBuilder.CreateTable(name: "MetodoPagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false).Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                }, constraints: table => { table.PrimaryKey("PK_MetodoPagos", x => x.Id); });
            migrationBuilder.CreateTable(name: "TipoInspecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false).Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                }, constraints: table => { table.PrimaryKey("PK_TipoInspecciones", x => x.Id); });

            // 2. Seed data for catalogs
            migrationBuilder.InsertData(table: "CondicionEjemplares", columns: new[] { "Id", "Nombre" }, values: new object[,] { { 1, "Nuevo" }, { 2, "Bueno" }, { 3, "Regular" } });
            migrationBuilder.InsertData(table: "EstadoEjemplares", columns: new[] { "Id", "Nombre" }, values: new object[,] { { 1, "Disponible" }, { 2, "Prestado" }, { 3, "Dañado" }, { 4, "Perdido" }, { 5, "Vendido" } });
            migrationBuilder.InsertData(table: "EstadoPrestamos", columns: new[] { "Id", "Nombre" }, values: new object[,] { { 1, "Activo" }, { 2, "Devuelto" }, { 3, "Vencido" }, { 4, "Cancelado" } });
            migrationBuilder.InsertData(table: "EstadoReservas", columns: new[] { "Id", "Nombre" }, values: new object[,] { { 1, "Pendiente" }, { 2, "Cumplida" }, { 3, "Cancelada" } });
            migrationBuilder.InsertData(table: "EstadoVentas", columns: new[] { "Id", "Nombre" }, values: new object[,] { { 1, "Completada" }, { 2, "Cancelada" } });
            migrationBuilder.InsertData(table: "MetodoPagos", columns: new[] { "Id", "Nombre" }, values: new object[,] { { 1, "Efectivo" }, { 2, "Tarjeta" }, { 3, "Transferencia" } });
            migrationBuilder.InsertData(table: "TipoInspecciones", columns: new[] { "Id", "Nombre" }, values: new object[,] { { 1, "Salida" }, { 2, "Entrada" } });

            // 3. Add new FK columns (nullable initially)
            migrationBuilder.AddColumn<int>(name: "EstadoEjemplarId", table: "Ejemplares", type: "integer", nullable: true);
            migrationBuilder.AddColumn<int>(name: "CondicionEjemplarId", table: "Ejemplares", type: "integer", nullable: true);
            migrationBuilder.AddColumn<int>(name: "EstadoPrestamoId", table: "Prestamos", type: "integer", nullable: true);
            migrationBuilder.AddColumn<int>(name: "EstadoReservaId", table: "Reservas", type: "integer", nullable: true);
            migrationBuilder.AddColumn<int>(name: "EstadoVentaId", table: "Ventas", type: "integer", nullable: true);
            migrationBuilder.AddColumn<int>(name: "MetodoPagoId", table: "Ventas", type: "integer", nullable: true);
            migrationBuilder.AddColumn<int>(name: "TipoInspeccionId", table: "Inspecciones", type: "integer", nullable: true);

            // 4. Migrate existing string values to FK IDs
            migrationBuilder.Sql(@"
                UPDATE ""Ejemplares"" SET ""EstadoEjemplarId"" = ee.""Id""
                FROM ""EstadoEjemplares"" ee WHERE ""Ejemplares"".""Estado"" = ee.""Nombre"";
                UPDATE ""Ejemplares"" SET ""CondicionEjemplarId"" = ce.""Id""
                FROM ""CondicionEjemplares"" ce WHERE ""Ejemplares"".""Condicion"" = ce.""Nombre"";
                UPDATE ""Prestamos"" SET ""EstadoPrestamoId"" = ep.""Id""
                FROM ""EstadoPrestamos"" ep WHERE ""Prestamos"".""Estado"" = ep.""Nombre"";
                UPDATE ""Reservas"" SET ""EstadoReservaId"" = er.""Id""
                FROM ""EstadoReservas"" er WHERE ""Reservas"".""Estado"" = er.""Nombre"";
                UPDATE ""Ventas"" SET ""EstadoVentaId"" = ev.""Id""
                FROM ""EstadoVentas"" ev WHERE ""Ventas"".""Estado"" = ev.""Nombre"";
                UPDATE ""Ventas"" SET ""MetodoPagoId"" = mp.""Id""
                FROM ""MetodoPagos"" mp WHERE ""Ventas"".""MetodoPago"" = mp.""Nombre"";
                UPDATE ""Inspecciones"" SET ""TipoInspeccionId"" = ti.""Id""
                FROM ""TipoInspecciones"" ti WHERE ""Inspecciones"".""TipoInspeccion"" = ti.""Nombre"";
            ");

            // 5. Make non-nullable FK columns NOT NULL (set default for any remaining nulls)
            migrationBuilder.Sql(@"UPDATE ""Ejemplares"" SET ""EstadoEjemplarId"" = 1 WHERE ""EstadoEjemplarId"" IS NULL;");
            migrationBuilder.AlterColumn<int>(name: "EstadoEjemplarId", table: "Ejemplares", type: "integer", nullable: false, oldNullable: true);

            migrationBuilder.Sql(@"UPDATE ""Prestamos"" SET ""EstadoPrestamoId"" = 1 WHERE ""EstadoPrestamoId"" IS NULL;");
            migrationBuilder.AlterColumn<int>(name: "EstadoPrestamoId", table: "Prestamos", type: "integer", nullable: false, oldNullable: true);

            migrationBuilder.Sql(@"UPDATE ""Reservas"" SET ""EstadoReservaId"" = 1 WHERE ""EstadoReservaId"" IS NULL;");
            migrationBuilder.AlterColumn<int>(name: "EstadoReservaId", table: "Reservas", type: "integer", nullable: false, oldNullable: true);

            migrationBuilder.Sql(@"UPDATE ""Ventas"" SET ""EstadoVentaId"" = 1 WHERE ""EstadoVentaId"" IS NULL;");
            migrationBuilder.AlterColumn<int>(name: "EstadoVentaId", table: "Ventas", type: "integer", nullable: false, oldNullable: true);

            migrationBuilder.Sql(@"UPDATE ""Ventas"" SET ""MetodoPagoId"" = 1 WHERE ""MetodoPagoId"" IS NULL;");
            migrationBuilder.AlterColumn<int>(name: "MetodoPagoId", table: "Ventas", type: "integer", nullable: false, oldNullable: true);

            migrationBuilder.Sql(@"UPDATE ""Inspecciones"" SET ""TipoInspeccionId"" = 1 WHERE ""TipoInspeccionId"" IS NULL;");
            migrationBuilder.AlterColumn<int>(name: "TipoInspeccionId", table: "Inspecciones", type: "integer", nullable: false, oldNullable: true);

            // 6. Drop old string columns + redundant columns
            migrationBuilder.DropColumn(name: "Estado", table: "Ejemplares");
            migrationBuilder.DropColumn(name: "Condicion", table: "Ejemplares");
            migrationBuilder.DropColumn(name: "Estado", table: "Prestamos");
            migrationBuilder.DropColumn(name: "Estado", table: "Reservas");
            migrationBuilder.DropColumn(name: "Estado", table: "Ventas");
            migrationBuilder.DropColumn(name: "MetodoPago", table: "Ventas");
            migrationBuilder.DropColumn(name: "TipoInspeccion", table: "Inspecciones");
            migrationBuilder.DropColumn(name: "Cantidad", table: "DetalleVentas");
            migrationBuilder.DropColumn(name: "CantidadEjemplares", table: "Libros");
            migrationBuilder.DropColumn(name: "EjemplaresDisponibles", table: "Libros");

            // 7. Create indexes
            migrationBuilder.CreateIndex(name: "IX_Ejemplares_EstadoEjemplarId", table: "Ejemplares", column: "EstadoEjemplarId");
            migrationBuilder.CreateIndex(name: "IX_Ejemplares_CondicionEjemplarId", table: "Ejemplares", column: "CondicionEjemplarId");
            migrationBuilder.CreateIndex(name: "IX_Prestamos_EstadoPrestamoId", table: "Prestamos", column: "EstadoPrestamoId");
            migrationBuilder.CreateIndex(name: "IX_Reservas_EstadoReservaId", table: "Reservas", column: "EstadoReservaId");
            migrationBuilder.CreateIndex(name: "IX_Ventas_EstadoVentaId", table: "Ventas", column: "EstadoVentaId");
            migrationBuilder.CreateIndex(name: "IX_Ventas_MetodoPagoId", table: "Ventas", column: "MetodoPagoId");
            migrationBuilder.CreateIndex(name: "IX_Inspecciones_TipoInspeccionId", table: "Inspecciones", column: "TipoInspeccionId");

            // 8. Add foreign keys
            migrationBuilder.AddForeignKey(name: "FK_Ejemplares_EstadoEjemplares_EstadoEjemplarId", table: "Ejemplares", column: "EstadoEjemplarId", principalTable: "EstadoEjemplares", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_Ejemplares_CondicionEjemplares_CondicionEjemplarId", table: "Ejemplares", column: "CondicionEjemplarId", principalTable: "CondicionEjemplares", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Prestamos_EstadoPrestamos_EstadoPrestamoId", table: "Prestamos", column: "EstadoPrestamoId", principalTable: "EstadoPrestamos", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_Reservas_EstadoReservas_EstadoReservaId", table: "Reservas", column: "EstadoReservaId", principalTable: "EstadoReservas", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_Ventas_EstadoVentas_EstadoVentaId", table: "Ventas", column: "EstadoVentaId", principalTable: "EstadoVentas", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_Ventas_MetodoPagos_MetodoPagoId", table: "Ventas", column: "MetodoPagoId", principalTable: "MetodoPagos", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_Inspecciones_TipoInspecciones_TipoInspeccionId", table: "Inspecciones", column: "TipoInspeccionId", principalTable: "TipoInspecciones", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ejemplares_CondicionEjemplares_CondicionEjemplarId",
                table: "Ejemplares");

            migrationBuilder.DropForeignKey(
                name: "FK_Ejemplares_EstadoEjemplares_EstadoEjemplarId",
                table: "Ejemplares");

            migrationBuilder.DropForeignKey(
                name: "FK_Inspecciones_TipoInspecciones_TipoInspeccionId",
                table: "Inspecciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_EstadoPrestamos_EstadoPrestamoId",
                table: "Prestamos");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_EstadoReservas_EstadoReservaId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_EstadoVentas_EstadoVentaId",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_MetodoPagos_MetodoPagoId",
                table: "Ventas");

            migrationBuilder.DropTable(
                name: "CondicionEjemplares");

            migrationBuilder.DropTable(
                name: "EstadoEjemplares");

            migrationBuilder.DropTable(
                name: "EstadoPrestamos");

            migrationBuilder.DropTable(
                name: "EstadoReservas");

            migrationBuilder.DropTable(
                name: "EstadoVentas");

            migrationBuilder.DropTable(
                name: "MetodoPagos");

            migrationBuilder.DropTable(
                name: "TipoInspecciones");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_EstadoVentaId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_MetodoPagoId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_EstadoReservaId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Prestamos_EstadoPrestamoId",
                table: "Prestamos");

            migrationBuilder.DropIndex(
                name: "IX_Inspecciones_TipoInspeccionId",
                table: "Inspecciones");

            migrationBuilder.DropIndex(
                name: "IX_Ejemplares_CondicionEjemplarId",
                table: "Ejemplares");

            migrationBuilder.DropIndex(
                name: "IX_Ejemplares_EstadoEjemplarId",
                table: "Ejemplares");

            migrationBuilder.DropColumn(
                name: "EstadoVentaId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "MetodoPagoId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "EstadoReservaId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "EstadoPrestamoId",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "TipoInspeccionId",
                table: "Inspecciones");

            migrationBuilder.DropColumn(
                name: "CondicionEjemplarId",
                table: "Ejemplares");

            migrationBuilder.DropColumn(
                name: "EstadoEjemplarId",
                table: "Ejemplares");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Ventas",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetodoPago",
                table: "Ventas",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Reservas",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Prestamos",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CantidadEjemplares",
                table: "Libros",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EjemplaresDisponibles",
                table: "Libros",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TipoInspeccion",
                table: "Inspecciones",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Condicion",
                table: "Ejemplares",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Ejemplares",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "DetalleVentas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
