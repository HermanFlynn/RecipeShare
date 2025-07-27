using RecipeShare.Util.Helpers.Classes;
using RecipeShare.Util.Models;

namespace RecipeShare.Util.Repository
{
	public interface IRepository<T>
		where T : IModel, new()
	{
		Task<T> AddAsync(T item);
		Task<T> UpdateAsync(T item);
		Task<T> DeleteAsync(string id, bool setDeleted = false);
		Task<T> GetAsync(string id, bool isDeleted = false);
		Task<List<T>> ListAsync(string? order = null, int pageNumber = 1, int itemsPerPage = 10, Search? search = null, bool isDeleted = false);
	}
}