using ClubeLeitura.ConsoleApp.ModuloRevista;
namespace ClubeLeitura.ConsoleApp.ModuloCategoria
{
    public class Categoria
    {
        public int numero;

        private readonly string nome;
        private readonly int diasEmprestimo;

        public Revista[] revistas;

        public string Nome => nome;

        public int DiasEmprestimo => diasEmprestimo;

        public Categoria(string nome, int diasEmprestimo)
        {
            this.nome = nome;
            this.diasEmprestimo = diasEmprestimo;
        }
    }
}
