using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeShare.Util.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CookingTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    DietaryTags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CookingTimeMinutes", "CreatedDateTime", "DietaryTags", "Ingredients", "IsDeleted", "ModifiedDateTime", "Title" },
                values: new object[,]
                {
                    { "26a9d2b6-733f-4d7c-9fa0-74da7fc480f7", 10, new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc), "[2,4,14]", "[\"Quinoa\",\"Cucumber\",\"Tomato\",\"Feta cheese\",\"Lemon dressing\"]", false, new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc), "Quinoa Salad" },
                    { "2de1e26e-3575-4727-85cf-b7ca6af6d07f", 15, new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc), "[2,4]", "[\"Mixed vegetables\",\"Soy sauce\",\"Garlic\",\"Ginger\",\"Sesame oil\"]", false, new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc), "Vegetable Stir Fry" },
                    { "bd9c31b2-d794-4603-8023-750263ca5405", 30, new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc), "[0]", "[\"Chicken\",\"Yogurt\",\"Tikka masala sauce\",\"Rice\",\"Cilantro\"]", false, new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc), "Chicken Tikka Masala" },
                    { "c612eaed-6258-489b-80c1-3103bb33d628", 20, new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc), "[0]", "[\"Spaghetti\",\"Bacon\",\"Eggs\",\"Parmesan cheese\",\"Black pepper\"]", false, new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc), "Spaghetti Carbonara" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
