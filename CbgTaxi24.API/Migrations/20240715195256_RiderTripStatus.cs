using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CbgTaxi24.API.Migrations
{
    /// <inheritdoc />
    public partial class RiderTripStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInTrip",
                table: "Riders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInTrip",
                table: "Riders");
        }
    }
}
