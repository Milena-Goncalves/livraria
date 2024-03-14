using System;
namespace projecto_iefp.Models;

public class Comentario
{

        public int UsuarioId {get; set;}
        public int LivrosId {get; set;}
        public string? LivroTitulo { get; set; }
        public string? Nome { get; set; }
        public string? ComentarioTexto { get; set; }

}

