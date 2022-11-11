namespace Client_Application.Model
{
	public class Cliente
	{
		public int	Id { get; set; }
		public string Nome { get; set; } = String.Empty;
		public Endereco Endereco { get; set; } = new Endereco();

	}
}
