using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio06
{
    public class Funcionario : Pessoa
    {
        public decimal Salario;

         public Funcionario(string nome, int idade, decimal salario)
           :base(nome, idade)
         { 

             Salario = salario;
         }

          public void ExibirDados()
        {

         Console.WriteLine($"eu sou {Nome}, tenho {Idade} anos e ganho um salario de {Salario}");

        }
    }
}  

