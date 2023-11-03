using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "datas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tpep_pickup_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tpep_dropoff_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    passenger_count = table.Column<byte>(type: "tinyint", nullable: false),
                    trip_distance = table.Column<float>(type: "real", nullable: false),
                    store_and_fwd_flag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PULocationID = table.Column<short>(type: "smallint", nullable: false),
                    DOLocationID = table.Column<short>(type: "smallint", nullable: false),
                    fare_amount = table.Column<float>(type: "real", nullable: false),
                    tip_amount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datas", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datas");
        }
    }
}
