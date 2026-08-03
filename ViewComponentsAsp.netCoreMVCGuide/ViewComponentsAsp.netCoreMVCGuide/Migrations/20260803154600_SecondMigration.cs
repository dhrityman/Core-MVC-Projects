using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asp.netCoreMVCIntro.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Tutorials_TutorialId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "TutorialId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleId", "ArticleContent", "ArticleTitle", "TutorialId" },
                values: new object[] { 1, "C# is an Object oriented language", "Introduction to C#", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Tutorials_TutorialId",
                table: "Articles",
                column: "TutorialId",
                principalTable: "Tutorials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Tutorials_TutorialId",
                table: "Articles");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "TutorialId",
                table: "Articles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Tutorials_TutorialId",
                table: "Articles",
                column: "TutorialId",
                principalTable: "Tutorials",
                principalColumn: "Id");
        }
    }
}
