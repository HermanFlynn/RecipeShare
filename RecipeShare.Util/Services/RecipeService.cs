using RecipeShare.Util.Helpers.Enums;
using RecipeShare.Util.Models.Database;
using RecipeShare.Util.Repository;

namespace RecipeShare.Util.Services
{
	public class RecipeService: IRecipeService
	{
		private readonly IRepository<Recipe> _recipeRepository;

		public RecipeService(IRepository<Recipe> recipeRepository)
		{
			_recipeRepository = recipeRepository;
		}

		public async Task<Recipe> CreateAsync(Recipe model, string id)
		{
			model.Id = id;
			model.CreatedDateTime = System.DateTime.UtcNow;
			model.ModifiedDateTime = System.DateTime.UtcNow;
			model.IsDeleted = false;

			var result = await _recipeRepository.AddAsync(model);
			result = model;
			await _recipeRepository.UpdateAsync(result);
			return model;
		}

		public async Task<Recipe> GetAsync(string id)
		{
			var result = await _recipeRepository.GetAsync(id);
			if (result == null) { return null; }
			Recipe _recipyDetails = result;
			return _recipyDetails;
		}

		public async Task<List<Recipe>> GetAllAsync()
		{
			var result = await _recipeRepository.ListAsync();
			List<Recipe> _recipies = result;
			return _recipies;
		}

		public async Task<List<Recipe>> FilterAsync(List<string> tags)
		{
			var result = await _recipeRepository.ListAsync();

			if (tags == null || !tags.Any())
			{
				return result.ToList();
			}

			var parsedTags = new HashSet<DietaryTagEnums>();
			foreach (var tagString in tags)
			{
				if (Enum.TryParse(tagString, true, out DietaryTagEnums enumTag))
				{
					parsedTags.Add(enumTag);
				}
			}

			if (!parsedTags.Any())
			{
				return result.ToList();
			}

			var filteredRecipes = result.Where(recipe => recipe.DietaryTags.Any(recipeTag => parsedTags.Contains(recipeTag))).ToList();

			return filteredRecipes;
		}

		public async Task<Recipe> UpdateAsync(Recipe model, string id)
		{
			var result = await _recipeRepository.GetAsync(id);

			result.Title = model.Title ?? result.Title;
			result.Ingredients = model.Ingredients ?? result.Ingredients;
			result.CookingTimeMinutes = model.CookingTimeMinutes;
			result.DietaryTags = model.DietaryTags ?? result.DietaryTags;
			result.ModifiedDateTime = System.DateTime.Today;

			await _recipeRepository.UpdateAsync(result);
			return result;
		}

		public async Task<Recipe> DeleteAsync(string id)
		{
			var result = await _recipeRepository.GetAsync(id);
			if (result == null) { return null; }
			result.IsDeleted = true;
			result.ModifiedDateTime = System.DateTime.Today;

			await _recipeRepository.UpdateAsync(result);
			return result;
		}
	}
}