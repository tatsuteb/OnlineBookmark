using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBookmark.Migrations.OnlineBookmarkDb
{
    public partial class CreateBookmarkAndBookmarkBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookmarkBases",
                columns: table => new
                {
                    BaseBid = table.Column<string>(nullable: false),
                    OwnerUid = table.Column<string>(nullable: false),
                    LinkedUrl = table.Column<string>(nullable: true),
                    ImageFilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookmarkBases", x => x.BaseBid);
                });

            migrationBuilder.CreateTable(
                name: "Bookmarks",
                columns: table => new
                {
                    Bid = table.Column<string>(nullable: false),
                    BaseBid = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmarks", x => x.Bid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookmarkBases");

            migrationBuilder.DropTable(
                name: "Bookmarks");
        }
    }
}
