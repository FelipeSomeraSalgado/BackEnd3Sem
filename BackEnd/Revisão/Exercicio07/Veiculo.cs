namespace Exercicio07
{
    public class Veiculo
    {
        // Método virtual, que pode ser sobrescrito pelas classes filhas
        public virtual void Mover()
        {
            Console.WriteLine("O veículo está se movendo...");
        }
    }
}
