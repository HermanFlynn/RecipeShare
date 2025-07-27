using Microsoft.EntityFrameworkCore;
using RecipeShare.Util.Config;
using RecipeShare.Util.Data;
using RecipeShare.Util.Helpers.Classes;
using RecipeShare.Util.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecipeShare.Util.Repository
{
	public class Repository<T> : IRepository<T>
		where T : class, IModel, new()
	{
		private readonly RecipeShareDbContext _context;
		private readonly DbSet<T> _dbSet;

		public Repository(RecipeShareDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public async Task<T> AddAsync(T item)
		{
			await _dbSet.AddAsync(item);
			await _context.SaveChangesAsync();
			return item;
		}

		public async Task<T> UpdateAsync(T item)
		{
			_dbSet.Update(item);
			await _context.SaveChangesAsync();
			return item;
		}

		public async Task<T> DeleteAsync(string id, bool setDeleted = false)
		{
			var item = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
			if (item == null) return null;

			if (setDeleted)
			{
				item.IsDeleted = setDeleted;
				_dbSet.Update(item);
			}
			else
			{
				_dbSet.Remove(item);
			}
			await _context.SaveChangesAsync();
			return item;
		}

		public async Task<T> GetAsync(string id, bool isDeleted = false)
		{
			return await _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == isDeleted);
		}

		public async Task<List<T>> ListAsync(string? order = null, int pageNumber = 1, int itemsPerPage = 10, Search? search = null, bool isDeleted = false)
		{
			IQueryable<T> query = _dbSet.Where(x => x.IsDeleted == isDeleted);

			if (search != null && search.Properties != null && !string.IsNullOrEmpty(search.SearchString))
			{
				var searchExpressions = new List<Expression<Func<T, bool>>>();
				foreach (var propName in search.Properties)
				{
					var parameter = Expression.Parameter(typeof(T), "x");
					var property = Expression.Property(parameter, propName);
					var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
					var constant = Expression.Constant(search.SearchString);
					var call = Expression.Call(property, containsMethod, constant);
					searchExpressions.Add(Expression.Lambda<Func<T, bool>>(call, parameter));
				}

				if (searchExpressions.Any())
				{
					var combinedSearchExpression = searchExpressions.Aggregate((acc, next) =>
					{
						var parameter = Expression.Parameter(typeof(T), "x");
						var body = Expression.OrElse(
							Expression.Invoke(acc, parameter),
							Expression.Invoke(next, parameter)
						);
						return Expression.Lambda<Func<T, bool>>(body, parameter);
					});
					query = query.Where(combinedSearchExpression);
				}
			}

			if (!string.IsNullOrEmpty(order))
			{
				var propertyInfo = typeof(T).GetProperty(order);
				if (propertyInfo != null)
				{
					query = query.OrderBy(x => EF.Property<object>(x, order));
				}
			}
			else
			{
				query = query.OrderBy(x => x.Id);
			}

			return await query
				.Skip((pageNumber - 1) * itemsPerPage)
				.Take(itemsPerPage)
				.ToListAsync();
		}
	}
}