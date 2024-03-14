using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projecto_iefp.Models;

namespace projecto_iefp.Pages
{
	public class CreateComentarioModel : PageModel
    {
        [BindProperty(Name = "titulo", SupportsGet = true)]
        public string Titulo { get; set; }

        [BindProperty(SupportsGet = true)]
        public string searchTerm { get; set; }

        public List<string> Titulos { get; set; }

        public IActionResult OnGet()
        {

            Titulo = Titulo;


            return RedirectToPage("Comentarios");
        }

        public void OnPost()
        {
            Comentario comentarios = new Comentario();

            comentarios.LivroTitulo = Titulo;
            comentarios.Nome = Request.Form["nome"];
            comentarios.ComentarioTexto = Request.Form["email"];
            comentarios.ComentarioTexto = Request.Form["comentario"];
            


            LivrosContext context = new LivrosContext();
            context.CreateComentario(comentarios);

            OnGet();
        }

    }
}
