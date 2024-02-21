using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Repository.Data.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Comments_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Taskats_Id",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Taskats_Id",
                table: "Comments",
                column: "Taskats_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Products_Taskats_Id",
                table: "Comments",
                column: "Taskats_Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Products_Taskats_Id",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_Taskats_Id",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Taskats_Id",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Comments_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
