using Client_Application.Model;

namespace Client_Application.Service
{
	public interface ICepService
	{
		public Task<Endereco> SearchCep(string Cep);
	}
}
