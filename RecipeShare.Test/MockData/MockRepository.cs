using RecipeShare.Util.Helpers.Classes;
using RecipeShare.Util.Models;
using RecipeShare.Util.Models.Database;
using RecipeShare.Util.Repository;

namespace RecipeShare.Test.MockData
{
	public class MockRepository<T> : IRepository<T> where T : BaseModel, new()
	{
		private readonly List<T> _data;

		public MockRepository(List<T> initialData = null)
		{
			_data = initialData ?? new List<T>();
		}

		public Task<T> AddAsync(T item)
		{
			if (string.IsNullOrEmpty(item.Id))
			{
				item.Id = Guid.NewGuid().ToString();
			}
			item.CreatedDateTime = DateTime.UtcNow;
			item.ModifiedDateTime = DateTime.UtcNow;
			_data.Add(item);
			return Task.FromResult(item);
		}

		public Task<T> UpdateAsync(T item)
		{
			var existingEntity = _data.FirstOrDefault(e => e.Id == item.Id);
			if (existingEntity == null)
			{
				throw new KeyNotFoundException($"Item with ID '{item.Id}' not found for update.");
			}

			if (existingEntity is Recipe existingRecipe && item is Recipe updatedRecipe)
			{
				existingRecipe.Title = updatedRecipe.Title;
				existingRecipe.Ingredients = updatedRecipe.Ingredients;
				existingRecipe.CookingTimeMinutes = updatedRecipe.CookingTimeMinutes;
				existingRecipe.DietaryTags = updatedRecipe.DietaryTags;
			}

			existingEntity.IsDeleted = item.IsDeleted;
			existingEntity.ModifiedDateTime = DateTime.UtcNow;

			return Task.FromResult(existingEntity);
		}

		public Task<T> DeleteAsync(string id, bool setDeleted = false)
		{
			var entityToDelete = _data.FirstOrDefault(e => e.Id == id);
			if (entityToDelete == null)
			{
				throw new KeyNotFoundException($"Item with ID '{id}' not found for deletion.");
			}

			entityToDelete.IsDeleted = setDeleted;
			entityToDelete.ModifiedDateTime = DateTime.UtcNow;

			return Task.FromResult(entityToDelete);
		}

		public Task<T> GetAsync(string id, bool isDeleted = false)
		{
			var entity = _data.FirstOrDefault(e => e.Id == id && e.IsDeleted == isDeleted);
			if (entity == null)
			{
				throw new KeyNotFoundException($"Item with ID '{id}' (IsDeleted: {isDeleted}) not found.");
			}
			return Task.FromResult(entity);
		}

		public Task<List<T>> ListAsync(string? order = null, int pageNumber = 1, int itemsPerPage = 10, Search? search = null, bool isDeleted = false)
		{
			IEnumerable<T> result = _data.Where(e => e.IsDeleted == isDeleted);

			if (search != null && !string.IsNullOrWhiteSpace(search.SearchString))
			{
				result = result.Where(e =>
					(e as Recipe)?.Title?.Contains(search.SearchString, StringComparison.OrdinalIgnoreCase) ?? false
				);
			}

			return Task.FromResult(result.ToList());
		}
	}
}