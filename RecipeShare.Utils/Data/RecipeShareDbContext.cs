using Microsoft.EntityFrameworkCore;
using RecipeShare.Utils.Helpers.Enums;
using RecipeShare.Utils.Models.Database;

namespace RecipeShare.Utils.Data
{
	public class RecipeShareDbContext: DbContext
	{
		public RecipeShareDbContext(DbContextOptions<RecipeShareDbContext> options)
			: base(options)
		{
		}

		public DbSet<Recipe> Recipes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Recipe>(entity =>
			{
				entity.HasKey(e => e.ID);
			});

			modelBuilder.Entity<Recipe>().HasData(new Recipe
			{
				Id = Guid.Parse("c612eaed-6258-489b-80c1-3103bb33d628"),
				Title = "Spaghetti Carbonara",
				Ingredients = new List<string>
				{
					"Spaghetti",
					"Bacon",
					"Eggs",
					"Parmesan cheese",
					"Black pepper"
				},
				CookingTimeMinutes = 20,
				DietaryTags = DietaryTagEnums.None,
				IsDeleted = false,
				CreatedDateTime = DateTime.UtcNow,
				ModifiedDateTime = DateTime.UtcNow
			});

			modelBuilder.Entity<Recipe>().HasData(new Recipe
			{
				Id = Guid.Parse("2de1e26e-3575-4727-85cf-b7ca6af6d07f"),
				Title = "Vegetable Stir Fry",
				Ingredients = new List<string>
				{
					"Mixed vegetables",
					"Soy sauce",
					"Garlic",
					"Ginger",
					"Sesame oil"
				},
				CookingTimeMinutes = 15,
				DietaryTags = DietaryTagEnums.Vegan | DietaryTagEnums.GlutenFree,
				IsDeleted = false,
				CreatedDateTime = DateTime.UtcNow,
				ModifiedDateTime = DateTime.UtcNow
			});

			modelBuilder.Entity<Recipe>().HasData(new Recipe
			{
				Id = Guid.Parse("bd9c31b2-d794-4603-8023-750263ca5405"),
				Title = "Chicken Tikka Masala",
				Ingredients = new List<string>
				{
					"Chicken",
					"Yogurt",
					"Tikka masala sauce",
					"Rice",
					"Cilantro"
				},
				CookingTimeMinutes = 30,
				DietaryTags = DietaryTagEnums.None,
				IsDeleted = false,
				CreatedDateTime = DateTime.UtcNow,
				ModifiedDateTime = DateTime.UtcNow
			});

			modelBuilder.Entity<Recipe>().HasData(new Recipe
			{
				Id = Guid.Parse("26a9d2b6-733f-4d7c-9fa0-74da7fc480f7"),
				Title = "Quinoa Salad",
				Ingredients = new List<string>
				{
					"Quinoa",
					"Cucumber",
					"Tomato",
					"Feta cheese",
					"Lemon dressing"
				},
				CookingTimeMinutes = 10,
				DietaryTags = DietaryTagEnums.Vegan | DietaryTagEnums.GlutenFree | DietaryTagEnums.Organic,
				IsDeleted = false,
				CreatedDateTime = DateTime.UtcNow,
				ModifiedDateTime = DateTime.UtcNow
			});
		}
	}
}