using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using projecto_iefp.Models;

namespace projecto_iefp.Pages
{

    public class DetailsModel : PageModel
    {
        public IEnumerable<Models.Comentario> comentario { get; set; }
        public livro livro { get; set; }

        public IActionResult OnGet(string passedObject)
        {

            livro = JsonConvert.DeserializeObject<livro>(passedObject);

            if (livro == null)
            {
                return NotFound();
            }
                
            return Page();
        }

        public void OnGetComentario(string searchTerm)
        {
            livrosContext context = new livrosContext();
            comentario = context.searchComentario(searchTerm);
            
        }
    }

}

