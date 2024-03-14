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
        Pedido pedidos = new Pedido();

            pedidos.LivroSinopse = Request.Form["sinopse"];
            pedidos.LivroCapa = Request.Form["capa"];
            pedidos.LivroAutor = Request.Form["autor"];
            pedidos.LivroEditora = Request.Form["editora"];
            pedidos.LivroGenero = Request.Form["genero"];

            LivrosContext context = new LivrosContext();
            context.CreateSugestao(pedidos);

            SuccessMessage = "Seu pedido foi enviado com sucesso! Nossos colaboradores irão avaliar e inseri-lo no site";

            return Page();
        }
        return Page();
    }


}


