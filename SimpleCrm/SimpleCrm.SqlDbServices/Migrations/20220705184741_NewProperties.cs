using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleCrm.SqlDbServices.Migrations
{
    public partial class NewProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Customer",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LastContactDate",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreferredContactMethod",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LastContactDate",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PreferredContactMethod",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Customer");
        }
    }
}
