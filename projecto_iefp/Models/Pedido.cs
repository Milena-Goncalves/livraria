using System;
namespace projecto_iefp.Models
{
	public class Pedido
	{
        public int UsuarioId {get; set;}
        public string? LivroTitulo { get; set; }
        public string? LivroSinopse { get; set; }
        public string? LivroCapa { get; set; }
        public string? LivroAutor { get; set; }
        public string? LivroEditora { get; set; }
        public string? LivroGenero { get; set; }
    }
}

