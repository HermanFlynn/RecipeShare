using RecipeShare.Util.Models.Database;

namespace RecipeShare.Util.Services
{
	public interface IRecipeService
	{
		Task<Recipe> CreateAsync(Recipe model, string entityId);
		Task<Recipe> GetAsync(string id);
		Task<List<Recipe>> GetAllAsync();
		Task<List<Recipe>> FilterAsync(List<string> tags);
		Task<Recipe> UpdateAsync(Recipe model, string entityId);
		Task<Recipe> DeleteAsync(string id);
	}
}