namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class Caixa
    {
        public int numero;

        private readonly string cor;
        private readonly string etiqueta;

        public string Cor => cor;

        public string Etiqueta => etiqueta;

        public Caixa(string cor, string etiqueta)
        {
            this.cor = cor;
            this.etiqueta = etiqueta;
        }
    }
}
