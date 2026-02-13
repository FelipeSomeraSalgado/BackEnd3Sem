using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio06
{
    public class Pessoa
    {
         public string Nome;
         public int Idade;


        public Pessoa(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }
        public void ExibirDados()
        {

         Console.WriteLine($"eu sou {Nome} e tenho {Idade} anos.");

        }

          public void Apresentar()
        {
            Console.WriteLine($"Olá, meu nome é {Nome}.");
        }

        // Sobrecarga: Apresentar com sobrenome
        public void Apresentar(string sobrenome)
        {
            Console.WriteLine($"Olá, meu nome é {Nome} {sobrenome}.");
        }
    }
} 
