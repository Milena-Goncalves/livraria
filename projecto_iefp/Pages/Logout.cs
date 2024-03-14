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
	public class LogoutModel : PageModel
    {   
        [HttpGet]
    public async Task<IActionResult> OnGetAsync()
    {
        
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        if(User.Identity.IsAuthenticated){
            ModelState.AddModelError(string.Empty, "Logout falou");
        }
        return RedirectToPage("/Index");
        
    } 
    }
} 