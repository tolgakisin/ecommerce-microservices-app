using Microsoft.EntityFrameworkCore.Migrations;

namespace Orchestrator.Migrations
{
    public partial class v103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventProcesses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventProcesses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventProcesses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventProcesses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EventProcesses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EventProcesses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "EventProcesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "OrderSubmittedEvent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EventProcesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "OrderFinishedEvent");

            migrationBuilder.InsertData(
                table: "EventProcesses",
                columns: new[] { "Id", "Name", "PreviousId" },
                values: new object[,]
                {
                    { 1, "Event1", null },
                    { 2, "Event2", 1 },
                    { 3, "Event3", null },
                    { 4, "Event4", 3 },
                    { 5, "Event5", 4 },
                    { 6, "Event6", null }
                });
        }
    }
}
