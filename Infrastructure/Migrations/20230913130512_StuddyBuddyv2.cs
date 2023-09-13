using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StuddyBuddyv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Teams_TeamEntityId",
                table: "Members");

            migrationBuilder.AlterColumn<int>(
                name: "TeamEntityId",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Teams_TeamEntityId",
                table: "Members",
                column: "TeamEntityId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Teams_TeamEntityId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Members");

            migrationBuilder.AlterColumn<int>(
                name: "TeamEntityId",
                table: "Members",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Teams_TeamEntityId",
                table: "Members",
                column: "TeamEntityId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
