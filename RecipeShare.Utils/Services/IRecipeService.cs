using RecipeShare.Utils.Models.Database;

namespace RecipeShare.Utils.Services
{
	public interface IRecipeService
	{
		Task<Recipe> CreateAsync(Recipe model, string entityId);
		Task<Recipe> GetAsync(string id);
		Task<List<Recipe>> GetAllAsync();
		Task<Recipe> UpdateAsync(Recipe model, string entityId);
		Task<Recipe> DeleteAsync(string id);
	}
}