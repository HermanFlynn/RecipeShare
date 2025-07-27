using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RecipeShare.Util.Helpers.Enums;
using RecipeShare.Util.Models.Database;

namespace RecipeShare.Util.Data
{
	public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<RecipeShareDbContext>
	{
		public RecipeShareDbContext CreateDbContext(string[] args)
		{
			// Build configuration (assuming appsettings.json exists in the same directory)
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("WebApiDatabase");

			if (string.IsNullOrEmpty(connectionString))
			{
				throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json. Please ensure it is correctly configured.");
			}
			var optionsBuilder = new DbContextOptionsBuilder<RecipeShareDbContext>();
			optionsBuilder.UseSqlServer(connectionString);

			return new RecipeShareDbContext(optionsBuilder.Options);
		}
	}

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
				entity.HasKey(e => e.Id);
			});

			modelBuilder.Entity<Recipe>().HasData(new Recipe
			{
				Id = "c612eaed-6258-489b-80c1-3103bb33d628",
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
				DietaryTags = new List<DietaryTagEnums> { DietaryTagEnums.None },
				IsDeleted = false,
				CreatedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, DateTimeKind.Utc),
				ModifiedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, DateTimeKind.Utc)
			});

			modelBuilder.Entity<Recipe>().HasData(new Recipe
			{
				Id = "2de1e26e-3575-4727-85cf-b7ca6af6d07f",
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
				DietaryTags = new List<DietaryTagEnums> { DietaryTagEnums.Vegan, DietaryTagEnums.GlutenFree },
				IsDeleted = false,
				CreatedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, DateTimeKind.Utc),
				ModifiedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, DateTimeKind.Utc)
			});

			modelBuilder.Entity<Recipe>().HasData(new Recipe
			{
				Id = "bd9c31b2-d794-4603-8023-750263ca5405",
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
				DietaryTags = new List<DietaryTagEnums> { DietaryTagEnums.None },
				IsDeleted = false,
				CreatedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, DateTimeKind.Utc),
				ModifiedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, DateTimeKind.Utc)
			});

			modelBuilder.Entity<Recipe>().HasData(new Recipe
			{
				Id = "26a9d2b6-733f-4d7c-9fa0-74da7fc480f7",
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
				DietaryTags = new List<DietaryTagEnums> { DietaryTagEnums.Vegan, DietaryTagEnums.GlutenFree, DietaryTagEnums.Organic },
				IsDeleted = false,
				CreatedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, DateTimeKind.Utc),
				ModifiedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, DateTimeKind.Utc)
			});
		}
	}
}