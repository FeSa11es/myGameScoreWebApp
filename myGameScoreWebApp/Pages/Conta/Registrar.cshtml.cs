using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace myGameScoreWebApp.Pages.Conta
{
    public class RegistrarModel : PageModel
    {
        public JogadorInfo jogadorinfo = new JogadorInfo();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            jogadorinfo.nome_user = Request.Form["nome_user"];
            jogadorinfo.email_user = Request.Form["email_user"];
            jogadorinfo.time_user = Request.Form["time_user"];

            if (jogadorinfo.nome_user.Length == 0 || jogadorinfo.email_user.Length == 0)
            {
                errorMsg = "Preencha todos os campos";
                return;
            }
            // adicionar no banco
            try
            {
                String connectionString = "Data Source =.; Initial Catalog = myGameScore; Integrated Security = True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "insert into TBUser(email_user, nome_user, time_user, dt_criacao) values"+
                                    "(@email_user,"+
                                    " @nome_user,"+
                                     "@time_user,"+
                                     "getdate());";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome_user", jogadorinfo.nome_user);
                        command.Parameters.AddWithValue("@email_user", jogadorinfo.email_user);
                        command.Parameters.AddWithValue("@time_user", jogadorinfo.time_user);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }


            successMsg = "Novo Jogador Registrado";
            Response.Redirect("/Conta/Pontuacao/IndexPontuacao");
        }
    }
}
