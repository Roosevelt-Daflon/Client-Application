using Client_Application.Model;
using NuGet.Protocol;

namespace Client_Application.Service
{
	public class ViaCepService : ICepService
	{
		public async Task<Endereco?> SearchCep(string Cep)
		{
			try
			{
				var httpClient = HttpClientFactory.Create();
				HttpResponseMessage response = await httpClient.GetAsync($"https://viacep.com.br/ws/{Cep}/json/");
				var endereco = response.Content.ReadAsAsync<Endereco>().Result;
				if(endereco.Cep == string.Empty)
					return null;
				return endereco;
			}
			catch
			{
				return null;
			}
			
		}
	}
}
