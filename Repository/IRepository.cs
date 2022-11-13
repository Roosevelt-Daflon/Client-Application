using Client_Application.Model;
using System.Linq.Expressions;

namespace Client_Application.Repository
{
	public interface IRepository
	{
		public Task<List<T>> GetAll<T>(params Expression<Func<T, object>>[] includeExpressions) where T : class;
		public Task<T?> GetBy<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions) where T : class;
		public Task<T> Add<T>(T item) where T : class;
		public Task<T> Update<T>(T item) where T : class;
		public Task<bool> DeleteById<T>(int id) where T : class;
		public Task<bool> CheckExists<T>(int id) where T : class;

	}
}
