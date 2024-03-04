using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projecto_iefp.Models;

namespace projecto_iefp.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class PedidoModel : PageModel
{
    public string SuccessMessage { get; set; }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            pedido pedidos = new pedido();

            pedidos.livro = Request.Form["livro"];
            pedidos.genero = Request.Form["genero"];
            pedidos.nome = Request.Form["nome"];
            pedidos.email = Request.Form["email"];


            livrosContext context = new livrosContext();
            context.createSugestao(pedidos);

            SuccessMessage = "Seu pedido foi enviado com sucesso! Nossos colaboradores irão avaliar e inseri-lo no site";

            return Page();
        }
        return Page();
    }


}


