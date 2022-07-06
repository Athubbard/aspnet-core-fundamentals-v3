using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleCrm.SqlDbServices.Migrations
{
    public partial class LastContactDateChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "LastContactDate",
                table: "Customer");
           
            migrationBuilder.AddColumn<int>(
                name: "LastContactDate",
                table: "Customer",
                type: "datetimeoffset",
                nullable: false );

        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LastContactDate",
                table: "Customer",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }
    }
}
