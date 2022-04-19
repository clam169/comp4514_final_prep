using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace final_mock.Data.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Votes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "FirstName", "Gender", "LastName", "Occupation", "PictureUrl", "Votes" },
                values: new object[] { 1, "John", "M", "Doe", "Student", "", 0 });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "FirstName", "Gender", "LastName", "Occupation", "PictureUrl", "Votes" },
                values: new object[] { 2, "Jane", "F", "Doe", "Student", "", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
