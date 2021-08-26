using Microsoft.EntityFrameworkCore.Migrations;

namespace MealPrepApp.Migrations.MealPrepDB
{
    public partial class AddedIngredientToMealTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ingredient",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ingredient",
                table: "Meals");
        }
    }
}
