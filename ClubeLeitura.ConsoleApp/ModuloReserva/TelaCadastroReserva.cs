using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using System;


namespace ClubeLeitura.ConsoleApp.ModuloReserva
{
    public class TelaCadastroReserva : Tela
    {
        private readonly Notificador notificador;
        private readonly RepositorioReserva repositorioReserva;
        private readonly RepositorioAmigo repositorioAmigo;
        private readonly RepositorioRevista repositorioRevista;
        private readonly TelaCadastroAmigo telaCadastroAmigo;
        private readonly TelaCadastroRevista telaCadastroRevista;
        private readonly RepositorioEmprestimo repositorioEmprestimo;

        public TelaCadastroReserva(
            Notificador notificador,
            RepositorioReserva repositorioReserva,
            RepositorioAmigo repositorioAmigo,
            RepositorioRevista repositorioRevista,
            TelaCadastroAmigo telaCadastroAmigo,
            TelaCadastroRevista telaCadastroRevista,
            RepositorioEmprestimo repositorioEmprestimo)
        {
            this.notificador = notificador;
            this.repositorioReserva = repositorioReserva;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
            this.telaCadastroAmigo = telaCadastroAmigo;
            this.telaCadastroRevista = telaCadastroRevista;
            this.repositorioEmprestimo = repositorioEmprestimo;
        }

        public override string MostrarOpcoes(string tipo)
        {
            Console.Clear();

            Console.WriteLine("Cadastro de Reservas");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Registrar Reserva");
            Console.WriteLine("Digite 2 para Visualizar");
            Console.WriteLine("Digite 3 para Visualizar Reservas em Aberto");
            Console.WriteLine("Digite 4 para Cadastrar um Empréstimo à partir de Reserva");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public override void Inserir()
        {
            MostrarTitulo("Inserindo nova Reserva");

            // Validação do Amigo
            Amigo amigoSelecionado = ObtemAmigo();

            if (amigoSelecionado.TemMultaEmAberto())
            {
                notificador.ApresentarMensagem("Este amigo tem uma multa em aberto e não pode reservar.", TipoMensagem.Erro);
                return;
            }

            if (amigoSelecionado.TemReservaEmAberto())
            {
                notificador.ApresentarMensagem("Este amigo já possui uma reserva em aberto..", TipoMensagem.Erro);
                return;
            }

            if (amigoSelecionado.TemEmprestimoEmAberto())
            {
                notificador.ApresentarMensagem("Este amigo já possui uma reserva em aberto e não pode reservar.", TipoMensagem.Erro);
                return;
            }

            // Validação da Revista
            Revista revistaSelecionada = ObtemRevista();

            if (revistaSelecionada.EstaReservada())
            {
                notificador.ApresentarMensagem("A revista selecionada já está reservada!", TipoMensagem.Erro);
                return;
            }

            if (revistaSelecionada.EstaEmprestada())
            {
                notificador.ApresentarMensagem("A revista selecionada já foi emprestada.", TipoMensagem.Erro);
                return;
            }

            Reserva novaReserva = ObtemReserva(amigoSelecionado, revistaSelecionada);

            repositorioReserva.Inserir(novaReserva);

            notificador.ApresentarMensagem("Reserva inserida com sucesso", TipoMensagem.Sucesso);
        }

        public void Registrar()
        {
            MostrarTitulo("Registrando novo Empréstimo");

            Reserva reservaParaEmprestimo = ObtemReservaParaEmprestimo();

            reservaParaEmprestimo.Fechar();

            Emprestimo novoEmprestimo = new Emprestimo();

            novoEmprestimo.revista = reservaParaEmprestimo.revista;
            novoEmprestimo.amigo = reservaParaEmprestimo.amigo;

            repositorioEmprestimo.Inserir(novoEmprestimo);
            
            notificador.ApresentarMensagem("Empréstimo registrado com sucesso", TipoMensagem.Sucesso);
        }

        public override bool Visualizar(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Reservas");

            Reserva[] reservas = repositorioReserva.SelecionarTodos();

            if (reservas.Length == 0)
                return false;

            for (int i = 0; i < reservas.Length; i++)
            {
                Reserva reserva = reservas[i];

                string statusReserva = reserva.estaAberta ? "Aberta" : "Fechada";

                Console.WriteLine("Número: " + reserva.numero);
                Console.WriteLine("Revista reservada: " + reserva.revista.Colecao);
                Console.WriteLine("Nome do amigo: " + reserva.amigo.Nome);
                Console.WriteLine("Data da reserva: " + reserva.dataInicialReserva.ToShortDateString());
                Console.WriteLine("Status da reserva: " + statusReserva);
                Console.WriteLine();
            }

            return true;
        }

        public bool VisualizarReservasEmAberto(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Reservas em Aberto");

            Reserva[] reservas = repositorioReserva.SelecionarReservasEmAberto();

            if (reservas.Length == 0)
                return false;

            for (int i = 0; i < reservas.Length; i++)
            {
                Reserva reserva = reservas[i];

                Console.WriteLine("Número: " + reserva.numero);
                Console.WriteLine("Revista reservada: " + reserva.revista.Colecao);
                Console.WriteLine("Nome do amigo: " + reserva.amigo.Nome);
                Console.WriteLine("Data de expiração da Reserva: " + reserva.dataInicialReserva.AddDays(2).ToShortDateString());
                Console.WriteLine();
            }

            return true;
        }

        private Reserva ObtemReserva(Amigo amigoSelecionado, Revista revistaSelecionada)
        {
            Reserva novaReserva = new Reserva();

            novaReserva.amigo = amigoSelecionado;
            novaReserva.revista = revistaSelecionada;

            return novaReserva;
        }

        public Reserva ObtemReservaParaEmprestimo()
        {
            bool temReservasEmAberto = VisualizarReservasEmAberto("Pesquisando");

            if (!temReservasEmAberto)
            {
                notificador.ApresentarMensagem("Não há nenhuma reserva aberta disponível para cadastro.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da reserva que irá emprestar: ");
            int numeroReserva = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Reserva reservaSelecionada = repositorioReserva.SelecionarReserva(numeroReserva);

            return reservaSelecionada;
        }

        public Amigo ObtemAmigo()
        {
            bool temAmigosDisponiveis = telaCadastroAmigo.Visualizar("Pesquisando");

            if (!temAmigosDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhum amigo disponível para cadastrar reservas.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do amigo que irá fazer a reserva: ");
            int numeroAmigoEmprestimo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Amigo amigoSelecionado = repositorioAmigo.SelecionarAmigo(numeroAmigoEmprestimo);

            return amigoSelecionado;
        }

        public Revista ObtemRevista()
        {
            bool temRevistasDisponiveis = telaCadastroRevista.Visualizar("Pesquisando");

            if (!temRevistasDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhuma revista disponível para cadastrar reservas.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da revista que será reservada: ");
            int numeroRevistaEmprestimo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Revista revistaSelecionada = repositorioRevista.SelecionarRevista(numeroRevistaEmprestimo);

            return revistaSelecionada;
        }

        protected override void MostrarTitulo(string titulo)
        {
            Console.Clear();

            Console.WriteLine(titulo);

            Console.WriteLine();
        }
    }
}
