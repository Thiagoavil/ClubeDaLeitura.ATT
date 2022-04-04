namespace ClubeLeitura.ConsoleApp.ModuloReserva
{
    public class RepositorioReserva : Repositorio
    {
        private readonly Reserva[] reservas;
        private int numeroReserva;

        public RepositorioReserva(int qtdReservas)
        {
            reservas = new Reserva[qtdReservas];
        }

        public void Inserir(Reserva reserva)
        {
            reserva.numero = ++numeroReserva;

            reserva.Abrir();
            reserva.revista.RegistrarReserva(reserva);
            reserva.amigo.RegistrarReserva(reserva);

            reservas[ObterPosicaoVazia()] = reserva;
        }

        public Reserva[] SelecionarTodos()
        {
            Reserva[] reservasInseridas = new Reserva[ObterQtdReservas()];

            int j = 0;

            for (int i = 0; i < reservasInseridas.Length; i++)
            {
                if (reservas[i] != null)
                {
                    reservasInseridas[j] = reservas[i];
                    j++;
                }
            }

            return reservasInseridas;
        }

        public Reserva[] SelecionarReservasEmAberto()
        {
            Reserva[] reservasInseridas = new Reserva[ObterQtdReservasEmAberto()];

            int j = 0;

            for (int i = 0; i < reservasInseridas.Length; i++)
            {
                if (reservas[i] != null && reservas[i].estaAberta)
                {
                    reservasInseridas[j] = reservas[i];
                    j++;
                }
            }

            return reservasInseridas;
        }

        public Reserva SelecionarReserva(int numeroReserva)
        {
            for (int i = 0; i < reservas.Length; i++)
            {
                if (reservas[i] != null && numeroReserva == reservas[i].numero)
                    return reservas[i];
            }

            return null;
        }

        public int ObterQtdReservas()
        {
            int numeroReservas = 0;

            for (int i = 0; i < reservas.Length; i++)
            {
                if (reservas[i] != null)
                    numeroReservas++;
            }

            return numeroReservas;
        }

        public int ObterQtdReservasEmAberto()
        {
            int numeroReservas = 0;

            for (int i = 0; i < reservas.Length; i++)
            {
                if (reservas[i] != null && reservas[i].estaAberta)
                    numeroReservas++;
            }

            return numeroReservas;
        }

        private int ObterPosicaoVazia()
        {
            for (int i = 0; i < reservas.Length; i++)
            {
                if (reservas[i] == null)
                    return i;
            }

            return -1;
        }
    }
}
