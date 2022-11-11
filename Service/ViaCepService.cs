using Client_Application.Model;

namespace Client_Application.Service
{
	public class ViaCepService : ICepService
	{
		public async Task<Endereco> SearchCep(string Cep)
		{
			var httpClient = HttpClientFactory.Create();
			HttpResponseMessage response = await httpClient.GetAsync($"https://viacep.com.br/ws/{Cep}/json/");
			var endereco = response.Content.ReadAsAsync<Endereco>().Result;
			return endereco;
		}
	}
}
