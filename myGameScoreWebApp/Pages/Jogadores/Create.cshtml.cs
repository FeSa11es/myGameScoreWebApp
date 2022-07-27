//Model Exemplo
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace myGameScoreWebApp.Pages.Jogadores
{
    public class CreateModel : PageModel
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

            if (jogadorinfo.nome_user.Length == 0 || jogadorinfo.email_user.Length == 0 )
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
                    String sql = "INSERT INTO TBUser " +
                                 "(nome_user,email_user) " +
                                 "values" +
                                 "(@nome_user,@email_user);";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@nome_user", jogadorinfo.nome_user);
                        command.Parameters.AddWithValue("@email_user", jogadorinfo.email_user);
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }


            jogadorinfo.nome_user = "";
            jogadorinfo.email_user = "";
            successMsg = "Novo Jogador Cadastrado";
        }
    }
}
