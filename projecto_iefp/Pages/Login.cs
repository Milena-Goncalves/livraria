using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projecto_iefp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace projecto_iefp.Pages
{
	public class LoginModel : PageModel
    {     

        [BindProperty]
        public string email { get; set; }

        [BindProperty]
        public string senha { get; set; }


    public async Task<IActionResult> OnPostAsync()
    {
    LivrosContext context = new LivrosContext();
    bool loginSuccessful = context.Login(email, senha);

    if (loginSuccessful)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, email),
        
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return RedirectToPage("/Index");
    }
    else
    {
        ModelState.AddModelError(string.Empty, "Login falhou. Por favor, verifique suas credenciais.");
        return Page();
    }
}
    }
}
