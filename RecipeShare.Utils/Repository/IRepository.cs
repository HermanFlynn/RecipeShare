using RecipeShare.Utils.Helpers.Classes;
using RecipeShare.Utils.Models;

namespace RecipeShare.Utils.Repository
{
	public interface IRepository<T>
		where T : IModel, new()
	{
		Task<T> AddAsync(T item);
		Task<T> UpdateAsync(T item);
		Task<T> DeleteAsync(Guid id, bool setDeleted = false);
		Task<T> GetAsync(Guid id, bool isDeleted = false);
		Task<List<T>> ListAsync(string? order = null, int pageNumber = 1, int itemsPerPage = 10, Search? search = null, bool isDeleted = false);
	}
}