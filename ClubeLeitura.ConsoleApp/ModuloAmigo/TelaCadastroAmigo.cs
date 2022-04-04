using ClubeLeitura.ConsoleApp.Compartilhado;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class TelaCadastroAmigo : Tela
    {
        private readonly Notificador notificador;
        private readonly RepositorioAmigo repositorioAmigo;

        public TelaCadastroAmigo(RepositorioAmigo repositorioAmigo, Notificador notificador)
        {
            this.repositorioAmigo = repositorioAmigo;
            this.notificador = notificador;
        }

        public override void OpcoesAdicionais()
        {
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar Amigos com Multa");
        }

        public void PagarMulta()
        {
            MostrarTitulo("Pagamento de Multas");

            bool temAmigosComMulta = VisualizarAmigosComMulta("Pesquisando");

            if (!temAmigosComMulta)
            {
                notificador.ApresentarMensagem("Não há nenhum amigo com multas em aberto", TipoMensagem.Atencao);
                return;
            }

            int numeroAmigoComMulta = ObterNumeroAmigo();

            Amigo amigoComMulta = repositorioAmigo.SelecionarAmigo(numeroAmigoComMulta);

            amigoComMulta.PagarMulta();
        }

        public override void Inserir()
        {
            MostrarTitulo("Inserindo novo amigo");

            Amigo novoAmigo = ObterAmigo();

            repositorioAmigo.Inserir(novoAmigo);

            notificador.ApresentarMensagem("Amigo cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public override void Editar()
        {
            MostrarTitulo("Editando Amigo");

            bool temAmigosCadastrados = Visualizar("Pesquisando");

            if (temAmigosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum amigo cadastrado para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroAmigo = ObterNumeroAmigo();

            Amigo amigoAtualizado = ObterAmigo();

            repositorioAmigo.Editar(numeroAmigo, amigoAtualizado);

            notificador.ApresentarMensagem("Amigo editado com sucesso", TipoMensagem.Sucesso);
        }


        public override void Excluir()
        {
            MostrarTitulo("Excluindo Amigo");

            bool temAmigosCadastrados = Visualizar("Pesquisando");

            if (temAmigosCadastrados == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhum amigo cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroAmigo = ObterNumeroAmigo();

            repositorioAmigo.Excluir(numeroAmigo);

            notificador.ApresentarMensagem("Amigo excluído com sucesso", TipoMensagem.Sucesso);
        }

        public override bool Visualizar(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Amigos");

            Amigo[] amigos = repositorioAmigo.SelecionarTodos();

            if (amigos.Length == 0)
                return false;

            for (int i = 0; i < amigos.Length; i++)
            {
                Amigo a = amigos[i];

                Console.WriteLine("Número: " + a.numero);
                Console.WriteLine("Nome: " + a.Nome);
                Console.WriteLine("Nome do responsável: " + a.NomeResponsavel);
                Console.WriteLine("Onde mora: " + a.Endereco);

                Console.WriteLine();
            }

            return true;
        }

        public bool VisualizarAmigosComMulta(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Amigos com Multa");

            Amigo[] amigos = repositorioAmigo.SelecionarAmigosComMulta();

            if (amigos.Length == 0)
                return false;

            for (int i = 0; i < amigos.Length; i++)
            {
                Amigo a = amigos[i];

                Console.WriteLine("Número: " + a.numero);
                Console.WriteLine("Nome: " + a.Nome);
                Console.WriteLine("Nome do responsável: " + a.NomeResponsavel);
                Console.WriteLine("Onde mora: " + a.Endereco);
                Console.WriteLine("Multa: R$" + a.multa.Valor);

                Console.WriteLine();
            }

            return true;
        }

        #region Métodos privados
        private int ObterNumeroAmigo()
        {
            int numeroAmigo;
            bool numeroAmigoEncontrado;

            do
            {
                Console.Write("Digite o número do amigo que deseja selecionar: ");
                numeroAmigo = Convert.ToInt32(Console.ReadLine());

                numeroAmigoEncontrado = repositorioAmigo.VerificarNumeroAmigoExiste(numeroAmigo);

                if (numeroAmigoEncontrado == false)
                    notificador.ApresentarMensagem("Número do amigo não encontrado, digite novamente.", TipoMensagem.Atencao);

            } while (numeroAmigoEncontrado == false);
            return numeroAmigo;
        }

        private Amigo ObterAmigo()
        {
            Console.Write("Digite o nome do amigo: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o número do telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite onde o amigo mora: ");
            string endereco = Console.ReadLine();

            Amigo amigo = new Amigo(nome, nomeResponsavel, telefone, endereco);

            return amigo;
        }

       
        #endregion
    }
}
