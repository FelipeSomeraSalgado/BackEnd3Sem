namespace LivrosBD.DTO
{
    public class LivrosDTO
    {
        public string? Titulo { get; set; }

        public IFormFile? Imagem { get; set; }

        public string? Autor { get; set; }

        public int? Ano { get; set; }

        public int IdGenero { get; set; }

    }
}
