using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeitura.ConsoleApp
{
    public abstract class Tela
    {
        public virtual string MostrarOpcoes(string tipo)
        {
            Console.Clear();

            Console.WriteLine("Cadastro de "+ tipo);

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            OpcoesAdicionais();
            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public abstract void Inserir();

        public virtual void Editar()
        {

        }

        public virtual void Excluir()
        {

        }

        public abstract bool Visualizar(string tipo);

        protected virtual void MostrarTitulo(string titulo)
        {
            Console.Clear();

            Console.WriteLine(titulo);

            Console.WriteLine();
        }

        public virtual void OpcoesAdicionais()
        {

        }

    }
}
