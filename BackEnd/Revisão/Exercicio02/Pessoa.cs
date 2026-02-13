using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio02
{
    public class Pessoa
    {
         public string Nome;
        public int Idade;

        public void ExibirDados()
        {
            Console.WriteLine($"eu sou{Nome}, e tenho {Idade} anos");
        }
    }
} 
