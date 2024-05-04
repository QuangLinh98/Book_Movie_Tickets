using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Movie_Tickets.Migrations
{
    /// <inheritdoc />
    public partial class FisrtMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    movies_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    release_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.movies_id);
                });

            migrationBuilder.CreateTable(
                name: "Theaters",
                columns: table => new
                {
                    theater_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theaters", x => x.theater_id);
                });

            migrationBuilder.CreateTable(
                name: "Screenings",
                columns: table => new
                {
                    screening_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    movie_id = table.Column<int>(type: "int", nullable: false),
                    theater_id = table.Column<int>(type: "int", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenings", x => x.screening_id);
                    table.ForeignKey(
                        name: "FK_Screenings_Movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "Movies",
                        principalColumn: "movies_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Screenings_Theaters_theater_id",
                        column: x => x.theater_id,
                        principalTable: "Theaters",
                        principalColumn: "theater_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    booking_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    screening_id = table.Column<int>(type: "int", nullable: false),
                    number_of_tickets = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.booking_id);
                    table.ForeignKey(
                        name: "FK_bookings_Customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookings_Screenings_screening_id",
                        column: x => x.screening_id,
                        principalTable: "Screenings",
                        principalColumn: "screening_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_customer_id",
                table: "bookings",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_screening_id",
                table: "bookings",
                column: "screening_id");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_movie_id",
                table: "Screenings",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_theater_id",
                table: "Screenings",
                column: "theater_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Screenings");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Theaters");
        }
    }
}
