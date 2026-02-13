using Exercicio03;

Pessoa mat = new Pessoa();
mat.Nome = "Matheus Becker";
mat.Idade = 0;

mat.ExibirDados();

if (mat.Idade >= 0)
{
    Console.WriteLine("Você já nasceu, parabéns");
}
else
{
    Console.WriteLine("Impossivel ter idade negativa!");
}
