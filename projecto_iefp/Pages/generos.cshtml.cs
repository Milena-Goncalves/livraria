using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projecto_iefp.Models;

namespace projecto_iefp.Pages
{
	public class generosModel : PageModel
    {
        public IEnumerable<Models.Livro> livros { get; set; }
        public void OnGet()
        {
            


        }


        public void OnGetEscolha(string genero)
        {
            ViewData["genero"] = genero;

            LivrosContext context = new LivrosContext();
            livros = context.GetAllGenero(genero);
        }
    }
}
