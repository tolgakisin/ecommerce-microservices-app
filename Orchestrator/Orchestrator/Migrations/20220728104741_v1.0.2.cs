using Microsoft.EntityFrameworkCore.Migrations;

namespace Orchestrator.Migrations
{
    public partial class v102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EventProcesses",
                columns: new[] { "Id", "Name", "PreviousId" },
                values: new object[,]
                {
                    { 7, "OrderCreatedEvent", null },
                    { 8, "OrderStartedEvent", null },
                    { 9, "OrderFinishedEvent", null },
                    { 10, "PaymentSuccessEvent", null },
                    { 11, "PaymentFailedEvent", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventProcesses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EventProcesses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EventProcesses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EventProcesses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EventProcesses",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
