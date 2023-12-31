﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingsWebApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table =>
                    new
                    {
                        Id = table.Column<string>(type: "TEXT", nullable: false),
                        Name = table.Column<string>(type: "TEXT", nullable: false),
                        State = table.Column<string>(type: "TEXT", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table =>
                    new
                    {
                        Id = table.Column<string>(type: "TEXT", nullable: false),
                        Name = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                        Email = table.Column<string>(type: "TEXT", nullable: false),
                        Password = table.Column<string>(
                            type: "TEXT",
                            maxLength: 25,
                            nullable: false
                        ),
                        Role = table.Column<string>(type: "TEXT", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table =>
                    new
                    {
                        Id = table.Column<string>(type: "TEXT", nullable: false),
                        Name = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                        Address = table.Column<string>(
                            type: "TEXT",
                            maxLength: 50,
                            nullable: false
                        ),
                        CityId = table.Column<string>(type: "TEXT", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table =>
                    new
                    {
                        Id = table.Column<string>(type: "TEXT", nullable: false),
                        Name = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                        Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                        Image = table.Column<string>(type: "TEXT", nullable: false),
                        HotelId = table.Column<string>(type: "TEXT", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table =>
                    new
                    {
                        Id = table.Column<string>(type: "TEXT", nullable: false),
                        CheckIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                        CheckOut = table.Column<DateTime>(type: "TEXT", nullable: false),
                        GuestQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                        UserId = table.Column<string>(type: "TEXT", nullable: false),
                        RoomId = table.Column<string>(type: "TEXT", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "State" },
                values: new object[] { "1", "City 1", "State 1" }
            );

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "State" },
                values: new object[] { "2", "City 2", "State 2" }
            );

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[] { "1", "user1@mail.com", "User 1", "Pass1", "Client" }
            );

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[] { "2", "user2@mail.com", "User 2", "Pass2", "Client" }
            );

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[] { "3", "user3@mail.com", "User 3", "Pass3", "Admin" }
            );

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "CityId", "Name" },
                values: new object[] { "1", "Address 1", "1", "Hotel 1" }
            );

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "CityId", "Name" },
                values: new object[] { "2", "Address 2", "2", "Hotel 2" }
            );

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "HotelId", "Image", "Name" },
                values: new object[] { "1", 2, "1", "Image 1", "Room 1" }
            );

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "HotelId", "Image", "Name" },
                values: new object[] { "2", 1, "1", "Image 2", "Room 2" }
            );

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "HotelId", "Image", "Name" },
                values: new object[] { "3", 3, "1", "Image 3", "Room 3" }
            );

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "HotelId", "Image", "Name" },
                values: new object[] { "4", 2, "2", "Image 4", "Room 4" }
            );

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "HotelId", "Image", "Name" },
                values: new object[] { "5", 1, "2", "Image 5", "Room 5" }
            );

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "HotelId", "Image", "Name" },
                values: new object[] { "6", 3, "2", "Image 6", "Room 6" }
            );

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CheckIn", "CheckOut", "GuestQuantity", "RoomId", "UserId" },
                values: new object[]
                {
                    "1",
                    new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2023, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    1,
                    "1",
                    "1"
                }
            );

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CheckIn", "CheckOut", "GuestQuantity", "RoomId", "UserId" },
                values: new object[]
                {
                    "2",
                    new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2023, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    1,
                    "2",
                    "1"
                }
            );

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CheckIn", "CheckOut", "GuestQuantity", "RoomId", "UserId" },
                values: new object[]
                {
                    "3",
                    new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2023, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    1,
                    "3",
                    "2"
                }
            );

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CheckIn", "CheckOut", "GuestQuantity", "RoomId", "UserId" },
                values: new object[]
                {
                    "4",
                    new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2023, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    1,
                    "4",
                    "2"
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CityId",
                table: "Hotels",
                column: "CityId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Bookings");

            migrationBuilder.DropTable(name: "Rooms");

            migrationBuilder.DropTable(name: "Users");

            migrationBuilder.DropTable(name: "Hotels");

            migrationBuilder.DropTable(name: "Cities");
        }
    }
}
