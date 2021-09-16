using Microsoft.EntityFrameworkCore.Migrations;

namespace GameApp.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Phone);
                });

            migrationBuilder.CreateTable(
                name: "WordsForUsers",
                columns: table => new
                {
                    UserPhone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordsForUsers", x => new { x.UserPhone, x.Word });
                    table.ForeignKey(
                        name: "FK_WordsForUsers_Users_UserPhone",
                        column: x => x.UserPhone,
                        principalTable: "Users",
                        principalColumn: "Phone",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordsForUsers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
