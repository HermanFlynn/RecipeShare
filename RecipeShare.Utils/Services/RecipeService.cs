using RecipeShare.Utils.Models.Database;
using RecipeShare.Utils.Repository;

namespace RecipeShare.Utils.Services
{
	public class RecipeService: IRecipeService
	{
		private readonly IRepository<Recipe> _recipyRepository;

		public RecipeService(IRepository<Recipe> recipyRepository)
		{
			_recipyRepository = recipyRepository;
		}

		public async Task<Recipe> CreateAsync(Recipe model, string id)
		{
			var result = await _recipyRepository.GetAsync(Guid.Parse(id));
			model.CreatedDateTime = System.DateTime.UtcNow;
			result = model;
			await _recipyRepository.UpdateAsync(result);
			return model;
		}

		public async Task<Recipe> GetAsync(string id)
		{
			var result = await _recipyRepository.GetAsync(Guid.Parse(id));
			Recipe _recipyDetails = result;
			return _recipyDetails;
		}

		public async Task<List<Recipe>> GetAllAsync()
		{
			var result = await _recipyRepository.ListAsync();
			List<Recipe> _recipies = result;
			return _recipies;
		}

		public async Task<Recipe> UpdateAsync(Recipe model, string id)
		{
			var result = await _recipyRepository.GetAsync(Guid.Parse(id));

			result.ID = model.ID;
			result.Title = model.Title;
			result.Ingredients = model.Ingredients;
			result.CookingTimeMinutes = model.CookingTimeMinutes;
			result.DietaryTags = model.DietaryTags;
			result.ModifiedDateTime = System.DateTime.Today;

			await _recipyRepository.UpdateAsync(result);
			return result;
		}

		public async Task<Recipe> DeleteAsync(string id)
		{
			var result = await _recipyRepository.GetAsync(Guid.Parse(id));

			result.IsDeleted = true;
			result.ModifiedDateTime = System.DateTime.Today;

			await _recipyRepository.UpdateAsync(result);
			return result;
		}
	}
}