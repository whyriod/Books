using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Migrations
{
    public partial class AddedPaymentRecieved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PaymentRecieved",
                table: "Purchases",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentRecieved",
                table: "Purchases");
        }
    }
}
