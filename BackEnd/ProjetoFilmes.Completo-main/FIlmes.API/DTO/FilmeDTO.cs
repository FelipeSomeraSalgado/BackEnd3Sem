namespace Filmes.API.DTO
{
    public class FilmeDTO
    {
        public string Titulo { get; set; }
        public IFormFile? Imagem { get; set; }
        public int IdGenero { get; set; }
    }
}
