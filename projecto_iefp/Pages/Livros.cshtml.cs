using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projecto_iefp.Models;

namespace projecto_iefp.Pages;

	public class LivrosModel : PageModel
{
    public IEnumerable<Models.Livro> livros { get; set; }

    public void OnGet()
    {
        LivrosContext context = new LivrosContext();
        livros = context.GetAllLivros();
        
    }

   
}
