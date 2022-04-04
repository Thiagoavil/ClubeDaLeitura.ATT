using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloReserva
{
    public class Reserva
    {
        public int numero;

        public Amigo amigo;
        public Revista revista;
        public DateTime dataInicialReserva;
        public bool estaAberta;

        public void Abrir()
        {
            if (!estaAberta)
            {
                estaAberta = true;
                dataInicialReserva = DateTime.Today;
            }
        }

        public void Fechar()
        {
            if (estaAberta)
                estaAberta = false;
        }

        public bool EstaExpirada()
        {
            bool ultrapassouDataReserva = dataInicialReserva.AddDays(2) > DateTime.Today;

            if (ultrapassouDataReserva)
                estaAberta = false;

            return ultrapassouDataReserva;
        }
    }
}
