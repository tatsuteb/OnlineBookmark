using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBookmark.Migrations.OnlineBookmarkDb
{
    public partial class CreateUserBookmark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserBookmarks",
                columns: table => new
                {
                    Seq = table.Column<decimal>(nullable: false),
                    Uid = table.Column<string>(nullable: false),
                    Bid = table.Column<string>(nullable: false),
                    IsPrivate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookmarks", x => x.Seq);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBookmarks");
        }
    }
}
