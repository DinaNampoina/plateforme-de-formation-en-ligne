using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OnelineLearning.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentIdToStudentProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProfiles_Students_StudentProfileId",
                table: "StudentProfiles");

            migrationBuilder.DropIndex(
                name: "IX_StudentProfiles_StudentProfileId",
                table: "StudentProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "StudentProfileId",
                table: "StudentProfiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "StudentProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_StudentId",
                table: "StudentProfiles",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProfiles_Students_StudentId",
                table: "StudentProfiles",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProfiles_Students_StudentId",
                table: "StudentProfiles");

            migrationBuilder.DropIndex(
                name: "IX_StudentProfiles_StudentId",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "StudentProfileId",
                table: "StudentProfiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_StudentProfileId",
                table: "StudentProfiles",
                column: "StudentProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProfiles_Students_StudentProfileId",
                table: "StudentProfiles",
                column: "StudentProfileId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
