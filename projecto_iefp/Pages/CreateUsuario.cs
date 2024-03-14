using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projecto_iefp.Models;

namespace projecto_iefp.Pages
{
	public class CreateUsuarioModel : PageModel
    {
        /*[BindProperty(Name = "titulo", SupportsGet = true)]*/
        

        [BindProperty(SupportsGet = true)]
        // public string searchTerm { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? Imagem { get; set; }

        /*public IActionResult OnGet()
        {

            Titulo = Titulo;


            return RedirectToPage("Comentarios");
        }*/

        public void OnPost()
        {
            Perfil perfil = new Perfil();

            perfil.Nome = Request.Form["nome"];
            perfil.Email = Request.Form["email"];
            perfil.Senha = Request.Form["senha"];
            perfil.Imagem = Request.Form["imagem"];
      
            


            LivrosContext context = new LivrosContext();
            context.CreatePerfil(perfil);
            RedirectToPage("Index");

        }

    }
}
