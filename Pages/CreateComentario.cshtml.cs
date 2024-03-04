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

            comentarios.titulo = Titulo;
            comentarios.nome = Request.Form["nome"];
            comentarios.email = Request.Form["email"];
            comentarios.comentario = Request.Form["comentario"];

            livrosContext context = new livrosContext();
            context.createComentario(comentarios);

            OnGet();
        }

    }
}
