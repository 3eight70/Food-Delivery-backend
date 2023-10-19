using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webNET_Hits_backend_aspnet_project_1.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInRatingAndActiveTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Dishes_DishId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_UserId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_DishId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Ratings",
                newName: "user");

            migrationBuilder.RenameColumn(
                name: "DishId",
                table: "Ratings",
                newName: "dish");

            migrationBuilder.AddColumn<Guid>(
                name: "RatingId",
                table: "Dishes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_RatingId",
                table: "Dishes",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Ratings_RatingId",
                table: "Dishes",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Ratings_RatingId",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_RatingId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "Dishes");

            migrationBuilder.RenameColumn(
                name: "user",
                table: "Ratings",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "dish",
                table: "Ratings",
                newName: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_DishId",
                table: "Ratings",
                column: "DishId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Dishes_DishId",
                table: "Ratings",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
