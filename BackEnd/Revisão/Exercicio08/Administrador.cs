using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio08
{
    public class Adiministrador : IAutenticavel
    {
          public bool autenticar(string Senha)
        {
            return Senha == "admin";
        }  
    }
}