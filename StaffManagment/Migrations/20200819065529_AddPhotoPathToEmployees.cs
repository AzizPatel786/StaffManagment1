﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace StaffManagment.Migrations
{
    public partial class AddPhotoPathToEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Staffs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Staffs");
        }
    }
}
