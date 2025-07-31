using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

public class LoginModel : PageModel
{
    [BindProperty] public string Username { get; set; }
    [BindProperty] public string Password { get; set; }

    public string ErrorMessage { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Username == "admin" && Password == "password")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Username)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToPage("/SecurePage");
        }

        ErrorMessage = "Invalid login attempt.";
        return Page();
    }
}