using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSOnline.Migrations
{
    /// <inheritdoc />
    public partial class DBInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCode = table.Column<string>(type: "varchar(200)", nullable: false),
                    gender = table.Column<bool>(type: "bit", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false),
                    Username = table.Column<string>(type: "varchar(200)", nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(12)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
