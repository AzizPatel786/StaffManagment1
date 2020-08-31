using Microsoft.EntityFrameworkCore.Migrations;

namespace StaffManagment.Migrations
{
    public partial class AlterStaffsSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "Id", "Department", "Email", "Name", "Occupation", "Subjects" },
                values: new object[] { 2, 1, "ac98741@avcol.school.nz", "Cooper", 1, 9 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
