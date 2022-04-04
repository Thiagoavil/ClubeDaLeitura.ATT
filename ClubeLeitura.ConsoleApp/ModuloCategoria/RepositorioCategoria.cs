namespace ClubeLeitura.ConsoleApp.ModuloCategoria
{
    public class RepositorioCategoria : Repositorio
    {
        private readonly Categoria[] categorias;
        private int numeroCategoria;

        public RepositorioCategoria(int qtdCategorias)
        {
            categorias = new Categoria[qtdCategorias];
        }

        public void Inserir(Categoria categoria)
        {
            categoria.numero = ++numeroCategoria;

            int posicaoVazia = ObterPosicaoVazia();

            categorias[posicaoVazia] = categoria;
        }

        public void Editar(int numeroSelecionado, Categoria categoria)
        {
            for (int i = 0; i < categorias.Length; i++)
            {
                if (categorias[i].numero == numeroSelecionado)
                {
                    categoria.numero = numeroSelecionado;
                    categorias[i] = categoria;

                    break;
                }
            }
        }

        public void Excluir(int numeroSelecionado)
        {
            for (int i = 0; i < categorias.Length; i++)
            {
                if (categorias[i].numero == numeroSelecionado)
                {
                    categorias[i] = null;
                    break;
                }
            }
        }

        public Categoria[] SelecionarTodos()
        {
            int quantidadeCategorias = ObterQtdCategorias();

            Categoria[] categoriasInseridas = new Categoria[quantidadeCategorias];

            int j = 0;

            for (int i = 0; i < categorias.Length; i++)
            {
                if (categorias[i] != null)
                {
                    categoriasInseridas[j] = categorias[i];
                    j++;
                }
            }

            return categoriasInseridas;
        }

        public Categoria SelecionarCategoria(int numeroCategoria)
        {
            for (int i = 0; i < categorias.Length; i++)
            {
                if (categorias[i] != null && numeroCategoria == categorias[i].numero)
                    return categorias[i];
            }

            return null;
        }

        public bool VerificarNumeroCategoriaExiste(int numeroCategoria)
        {
            bool numeroRevistaEncontrado = false;

            for (int i = 0; i < categorias.Length; i++)
            {
                if (categorias[i] != null && categorias[i].numero == numeroCategoria)
                {
                    numeroRevistaEncontrado = true;
                    break;
                }
            }

            return numeroRevistaEncontrado;
        }

        #region Métodos privados
        private int ObterPosicaoVazia()
        {
            for (int i = 0; i < categorias.Length; i++)
            {
                if (categorias[i] == null)
                    return i;
            }

            return -1;
        }

        private int ObterQtdCategorias()
        {
            int numeroCategorias = 0;

            for (int i = 0; i < categorias.Length; i++)
            {
                if (categorias[i] != null)
                    numeroCategorias++;
            }

            return numeroCategorias;
        }
        #endregion
    }
}
