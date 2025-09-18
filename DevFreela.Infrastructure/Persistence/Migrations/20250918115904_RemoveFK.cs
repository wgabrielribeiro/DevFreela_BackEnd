using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevFreela.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsComments_Users_UserId",
                table: "ProjectsComments");

            migrationBuilder.DropIndex(
                name: "IX_ProjectsComments_UserId",
                table: "ProjectsComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProjectsComments");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsComments_IdUser",
                table: "ProjectsComments",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsComments_Users_IdUser",
                table: "ProjectsComments",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsComments_Users_IdUser",
                table: "ProjectsComments");

            migrationBuilder.DropIndex(
                name: "IX_ProjectsComments_IdUser",
                table: "ProjectsComments");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ProjectsComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsComments_UserId",
                table: "ProjectsComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsComments_Users_UserId",
                table: "ProjectsComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
