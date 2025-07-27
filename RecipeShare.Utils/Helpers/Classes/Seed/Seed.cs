using Microsoft.EntityFrameworkCore;
using RecipeShare.Utils.Config;
using RecipeShare.Utils.Data;
using RecipeShare.Utils.Models.Database;
using RecipeShare.Utils.Services;
using System.Collections.Generic;

namespace RecipeShare.Utils.Helpers.Classes.Seed
{
	public class Seed : ISeed
	{
		private readonly IRecipeService _recipeService;
		private readonly RecipeShareDbContext _context;

		public Seed(IRecipeService recipeService, RecipeShareDbContext context)
		{
			_recipeService = recipeService;
			_context = context;
		}

		public void SeedAll(DatabaseConfig databaseConfig, SeedConfig seedConfig)
		{
			SeedRecipes(seedConfig.Recipes).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		private async Task SeedRecipes(List<Recipe> recipes)
		{
			if (await _context.Recipes.AnyAsync())
			{
				return;
			}

			if (recipes != null && recipes.Any())
			{
				foreach (var recipe in recipes)
				{
					await _recipeService.CreateAsync(recipe, Guid.NewGuid().ToString());
				}
			}
		}
	}
}