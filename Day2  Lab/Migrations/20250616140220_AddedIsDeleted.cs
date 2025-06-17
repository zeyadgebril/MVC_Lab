using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Day2__Lab.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsDeleted",
                table: "Trainee",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "instructor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsDeleted",
                table: "instructor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsDeleted",
                table: "Department",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsDeleted",
                table: "crsResult",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsDeleted",
                table: "Course",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Trainee");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "instructor");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "crsResult");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Course");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "instructor",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
