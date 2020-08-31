using Microsoft.EntityFrameworkCore.Migrations;

namespace StaffManagment.Migrations
{
    public partial class SeedStaffsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "Id", "Department", "Email", "Name", "Occupation", "Subjects" },
                values: new object[] { 1, 2, "ac98844@avcol.school.nz", "Aziz", 1, 8 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
