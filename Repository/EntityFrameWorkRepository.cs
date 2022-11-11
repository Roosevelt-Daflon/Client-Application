using Client_Application.Data;
using Client_Application.Model;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Linq.Expressions;

namespace Client_Application.Repository
{
	public class EntityFrameWorkRepository : IRepository
	{
		protected readonly DataContext _Context;

		public EntityFrameWorkRepository(DataContext context)
		{
			_Context = context;
		}

		public async Task<T> Add<T>(T item) where T : class
		{
			_Context.Set<T>().Add(item);
			await _Context.SaveChangesAsync();
			return item;
		}

		public async Task<bool> CheckExists<T>(int id) where T: class
		{
			var data = await _Context.Set<T>().FindAsync(id);
			return data != null;
		}

		public async Task<bool> DeleteById<T>(int id) where T : class
		{
			if(await CheckExists<T>(id))
			{
				var item = await _Context.Set<T>().FindAsync(id);
				_Context.Set<T>().Remove(item);
				await _Context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<List<T>> GetAll<T>(params Expression<Func<T, object>>[] includeExpressions) where T : class
		{

			IQueryable<T> set = _Context.Set<T>();

			foreach(var includeExpression in includeExpressions)
			{
				set = set.Include(includeExpression);
			}

			return await set.ToListAsync();
		}

		public async Task<T> GetBy<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions) where T : class
		{
			IQueryable<T> set = _Context.Set<T>();
			if(includeExpressions.Any())
			{
				foreach (var includeExpression in includeExpressions)
				{
					set = set.Include(includeExpression);
				}

			}

			return set.FirstOrDefault(predicate);
		}

		public async Task<T> Update<T>(int id, T item) where T : class
		{
			_Context.Set<T>().Update(item);
			await _Context.SaveChangesAsync();
			return item;

		}		
	}
}
