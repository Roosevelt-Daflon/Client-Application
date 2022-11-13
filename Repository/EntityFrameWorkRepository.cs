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

		//-> T é o tipo generico a ser usado, na qual deve ser associado a uma classe na hora de usar a função
		public async Task<T> Add<T>(T item) where T : class
		{
			_Context.Set<T>().Add(item);
			await _Context.SaveChangesAsync();
			return item;
		}

		//-> T é o tipo generico a ser usado, na qual deve ser associado a uma classe na hora de usar a função
		public async Task<bool> CheckExists<T>(int id) where T: class
		{
			var data = await _Context.Set<T>().FindAsync(id);
			return data != null;
		}

		//-> T é o tipo generico a ser usado, na qual deve ser associado a uma classe na hora de usar a função
		public async Task<bool> DeleteById<T>(int id) where T : class
		{
			if(await CheckExists<T>(id))
			{
				var item = await _Context.Set<T>().FindAsync(id);
				if(item != null)
				{
					_Context.Set<T>().Remove(item);
					await _Context.SaveChangesAsync();
					return true;
				}
				
			}
			return false;
		}
		/*função getAll genérica
		 * ->Parametro ->includeExpressions<- é os includes parar a ORM conseguir vincular os elemetos necessários  
		 * -> T é o tipo generico a ser usado, na qual deve ser associado a uma classe na hora de usar a função
		*/
		public async Task<List<T>> GetAll<T>(params Expression<Func<T, object>>[] includeExpressions) where T : class
		{

			IQueryable<T> set = _Context.Set<T>();

			foreach(var includeExpression in includeExpressions)
			{
				set = set.Include(includeExpression);
			}

			return await set.ToListAsync();
		}

		/*função getBy genérica
		 * ->Paremetro ->predicate<- é o filtro para o get;
		 * ->Parametro ->includeExpressions<- é os includes parar a ORM conseguir vincular os elemetos necessários  
		 *-> T é o tipo generico a ser usado, na qual deve ser associado a uma classe na hora de usar a função
		*/
		public async Task<T?> GetBy<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions) where T : class
		{
			IQueryable<T> set = _Context.Set<T>();
			if(includeExpressions.Any())
			{
				foreach (var includeExpression in includeExpressions)
				{
					set = set.Include(includeExpression);
				}

			}

			return await set.FirstOrDefaultAsync(predicate);
		}
		//-> T é o tipo generico a ser usado, na qual deve ser associado a uma classe na hora de usar a função
		public async Task<T> Update<T>(T item) where T : class
		{
			_Context.Set<T>().Update(item);
			await _Context.SaveChangesAsync();
			return item;

		}		
	}
}
