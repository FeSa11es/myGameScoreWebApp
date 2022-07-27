using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace myGameScoreWebApp.Pages.Conta
{
    
    public class PerfilModel : PageModel
    {
        public int id_conectado = 0;
        public void OnGet()
        {
        }
    }
    public class JogadorInfo
    {
        public string id_user;
        public string nome_user;
        public string email_user;
        public string time_user;
    }


}

