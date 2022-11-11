using System.Diagnostics.CodeAnalysis;

namespace Client_Application.Model
{
	public class Endereco
	{

		public int Id { get; set; }

		[AllowNull]
		public string? Cep { get; set; } = string.Empty;

		[AllowNull]
		public string? Logradouro { get;set; } = string.Empty;

		[AllowNull]
		public string? Complemento { get; set; } = string.Empty;

		[AllowNull]
		public string? Bairro { get; set; } = string.Empty;

		[AllowNull]
		public string? Localidade { get; set; } = string.Empty;

		[AllowNull]
		public string? Uf { get; set; } = string.Empty;

	}
}