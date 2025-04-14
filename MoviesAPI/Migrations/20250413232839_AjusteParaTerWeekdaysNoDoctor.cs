using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AjusteParaTerWeekdaysNoDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeekDays",
                table: "Doctors",
                newName: "Surname");

            migrationBuilder.AddColumn<int>(
                name: "WeekdaysId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Weekdays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monday = table.Column<bool>(type: "bit", nullable: false),
                    Sunday = table.Column<bool>(type: "bit", nullable: false),
                    Tuesday = table.Column<bool>(type: "bit", nullable: false),
                    Wednesday = table.Column<bool>(type: "bit", nullable: false),
                    Thursday = table.Column<bool>(type: "bit", nullable: false),
                    Friday = table.Column<bool>(type: "bit", nullable: false),
                    Saturday = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weekdays", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_WeekdaysId",
                table: "Doctors",
                column: "WeekdaysId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Weekdays_WeekdaysId",
                table: "Doctors",
                column: "WeekdaysId",
                principalTable: "Weekdays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Weekdays_WeekdaysId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Weekdays");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_WeekdaysId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "WeekdaysId",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Doctors",
                newName: "WeekDays");
        }
    }
}
