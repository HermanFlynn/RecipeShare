using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using RecipeShare.Test.MockData;
using RecipeShare.Util.Helpers.Enums;
using RecipeShare.Util.Models.Database;
using RecipeShare.Util.Repository;
using RecipeShare.Util.Services;

namespace RecipeShare.Test
{
	public class RecipeServiceTest : IDisposable
	{
		private IRepository<Recipe> _recipeRepository;
		private IRecipeService _recipeService;
		private List<Recipe> _initialRecipes;
		private readonly HttpClient _httpClient;

		public RecipeServiceTest()
		{
			_httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5154") };

			_initialRecipes = new List<Recipe>
			{
				new Recipe
				{
					Id = "recipe1",
					Title = "Spaghetti Carbonara",
					Ingredients = new List<string> { "Spaghetti", "Bacon" },
					CookingTimeMinutes = 20,
					DietaryTags = new List<DietaryTagEnums> { DietaryTagEnums.None },
					IsDeleted = false,
					CreatedDateTime = DateTime.UtcNow,
					ModifiedDateTime = DateTime.UtcNow
				},
				new Recipe
				{
					Id = "recipe2",
					Title = "Vegetable Stir Fry",
					Ingredients = new List<string> { "Mixed vegetables", "Soy sauce" },
					CookingTimeMinutes = 15,
					DietaryTags = new List<DietaryTagEnums> { DietaryTagEnums.Vegan, DietaryTagEnums.GlutenFree },
					IsDeleted = false,
					CreatedDateTime = DateTime.UtcNow,
					ModifiedDateTime = DateTime.UtcNow
				},
				new Recipe
				{
					Id = "recipe3",
					Title = "Chicken Tikka Masala",
					Ingredients = new List<string> { "Chicken", "Yogurt" },
					CookingTimeMinutes = 30,
					DietaryTags = new List<DietaryTagEnums> { DietaryTagEnums.None },
					IsDeleted = false,
					CreatedDateTime = DateTime.UtcNow,
					ModifiedDateTime = DateTime.UtcNow
				},
				 new Recipe
				{
					Id = "recipe4",
					Title = "Quinoa Salad",
					Ingredients = new List<string> { "Quinoa", "Cucumber", "Tomato" },
					CookingTimeMinutes = 10,
					DietaryTags = new List<DietaryTagEnums> { DietaryTagEnums.Vegan, DietaryTagEnums.GlutenFree, DietaryTagEnums.Organic },
					IsDeleted = false,
					CreatedDateTime = DateTime.UtcNow,
					ModifiedDateTime = DateTime.UtcNow
				}
			};

			_recipeRepository = new MockRepository<Recipe>(_initialRecipes);
			_recipeService = new RecipeService(_recipeRepository);
		}

		public void Dispose()
		{
			_recipeRepository = null;
			_recipeService = null;
			_initialRecipes = null;
		}

		[Benchmark]
		public async Task GetRecipes()
		{
			for (int i = 0; i < 500; i++)
			{
				var response = await _httpClient.GetAsync("/readAll");
				response.EnsureSuccessStatusCode();
			}
		}

		[Fact]
		public async Task CreateAsync_ShouldAddRecipeAndReturnIt()
		{
			var newRecipe = new Recipe
			{
				Title = "New Test Recipe",
				Ingredients = new List<string> { "Ingredient A", "Ingredient B" },
				CookingTimeMinutes = 45,
				DietaryTags = new List<DietaryTagEnums> { DietaryTagEnums.Vegetarian },
				IsDeleted = false
			};

			var createdRecipe = await _recipeService.CreateAsync(newRecipe, Guid.NewGuid().ToString());

			Assert.NotNull(createdRecipe);
			Assert.False(string.IsNullOrEmpty(createdRecipe.Id));
			Assert.Equal("New Test Recipe", createdRecipe.Title);
			Assert.Contains(createdRecipe, await _recipeRepository.ListAsync());
		}

		[Fact]
		public async Task GetAsync_ShouldReturnExistingRecipe()
		{
			var expectedRecipeId = "recipe1";

			var recipe = await _recipeService.GetAsync(expectedRecipeId);

			Assert.NotNull(recipe);
			Assert.Equal(expectedRecipeId, recipe.Id);
			Assert.Equal("Spaghetti Carbonara", recipe.Title);
		}

		[Fact]
		public async Task GetAsync_ShouldReturnNullForNonExistentRecipe()
		{
			var nonExistentId = "nonexistent";

			var recipe = await _recipeService.GetAsync(nonExistentId);

			Assert.Null(recipe);
		}

		[Fact]
		public async Task GetAllAsync_ShouldReturnAllNonDeletedRecipes()
		{
			_initialRecipes.First(r => r.Id == "recipe3").IsDeleted = true;

			var recipes = await _recipeService.GetAllAsync();

			Assert.NotNull(recipes);
			Assert.Equal(3, recipes.Count());
			Assert.DoesNotContain(recipes, r => r.Id == "recipe3");
		}

		[Fact]
		public async Task UpdateAsync_ShouldUpdateExistingRecipe()
		{
			var recipeToUpdate = _initialRecipes.First(r => r.Id == "recipe2");
			recipeToUpdate.Title = "Updated Stir Fry";
			recipeToUpdate.CookingTimeMinutes = 25;
			recipeToUpdate.DietaryTags = new List<DietaryTagEnums> { DietaryTagEnums.Vegetarian };

			var updatedRecipe = await _recipeService.UpdateAsync(recipeToUpdate, recipeToUpdate.Id);

			Assert.NotNull(updatedRecipe);
			Assert.Equal("Updated Stir Fry", updatedRecipe.Title);
			Assert.Equal(25, updatedRecipe.CookingTimeMinutes);
			Assert.Contains(DietaryTagEnums.Vegetarian, updatedRecipe.DietaryTags);
			Assert.DoesNotContain(DietaryTagEnums.Vegan, updatedRecipe.DietaryTags);
		}

		[Fact]
		public async Task UpdateAsync_ShouldReturnNullForNonExistentRecipe()
		{
			var nonExistentRecipe = new Recipe { Id = "nonexistent", Title = "Fake" };

			var updatedRecipe = await _recipeService.UpdateAsync(nonExistentRecipe, nonExistentRecipe.Id);

			Assert.Null(updatedRecipe);
		}

		[Fact]
		public async Task DeleteAsync_ShouldMarkRecipeAsDeleted()
		{
			var recipeIdToDelete = "recipe1";

			var deletedRecipe = await _recipeService.DeleteAsync(recipeIdToDelete);

			Assert.NotNull(deletedRecipe);
			Assert.True(deletedRecipe.IsDeleted);
			Assert.Null(await _recipeService.GetAsync(recipeIdToDelete));
		}

		[Fact]
		public async Task DeleteAsync_ShouldReturnNullForNonExistentRecipe()
		{
			var nonExistentId = "nonexistent";

			var deletedRecipe = await _recipeService.DeleteAsync(nonExistentId);

			Assert.Null(deletedRecipe);
		}

		[Fact]
		public async Task FilterAsync_ShouldReturnRecipesMatchingSingleTag()
		{
			var tagsToFilter = new List<string> { "Vegan" };

			var filteredRecipes = await _recipeService.FilterAsync(tagsToFilter);

			Assert.NotNull(filteredRecipes);
			Assert.Equal(2, filteredRecipes.Count);
			Assert.Contains(filteredRecipes, r => r.Id == "recipe2");
			Assert.Contains(filteredRecipes, r => r.Id == "recipe4");
		}

		[Fact]
		public async Task FilterAsync_ShouldReturnRecipesMatchingMultipleTags()
		{
			var tagsToFilter = new List<string> { "GlutenFree", "Organic" };

			var filteredRecipes = await _recipeService.FilterAsync(tagsToFilter);

			Assert.NotNull(filteredRecipes);
			Assert.Equal(2, filteredRecipes.Count);
			Assert.Contains(filteredRecipes, r => r.Id == "recipe2");
			Assert.Contains(filteredRecipes, r => r.Id == "recipe4");
		}

		[Fact]
		public async Task FilterAsync_ShouldReturnAllRecipesIfNoTagsProvided()
		{
			var tagsToFilter = new List<string>();

			var filteredRecipes = await _recipeService.FilterAsync(tagsToFilter);

			Assert.NotNull(filteredRecipes);
			Assert.Equal(_initialRecipes.Count(r => (bool)!r.IsDeleted), filteredRecipes.Count); // All non-deleted recipes
		}

		[Fact]
		public async Task FilterAsync_ShouldReturnEmptyListIfNoMatchingRecipes()
		{
			var tagsToFilter = new List<string> { "NonExistentTag" };

			var filteredRecipes = await _recipeService.FilterAsync(tagsToFilter);

			Assert.NotNull(filteredRecipes);
			Assert.Empty(filteredRecipes);
		}
	}
}