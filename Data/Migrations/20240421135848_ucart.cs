using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBMS_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class ucart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Cart",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cart");
        }
    }
}
