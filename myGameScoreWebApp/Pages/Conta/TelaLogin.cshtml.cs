using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace myGameScoreWebApp.Pages.Conta
{
    public class TelaLoginModel : PageModel
    {
        public JogadorInfo jogadorinfo = new JogadorInfo();
        public String errorMsg = "";
        public String successMsg = "";
        public int id_conectado = 0;
        private Boolean resultadoComando = false;
        public Boolean Logado = false;
        public void OnPost()
        {
            jogadorinfo.nome_user = Request.Form["nome_user"];
            jogadorinfo.email_user = Request.Form["email_user"];

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
                    String sql = "select count(*) from TBUser where email_user = @email_user and nome_user = @nome_user";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome_user", jogadorinfo.nome_user);
                        command.Parameters.AddWithValue("@email_user", jogadorinfo.email_user);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                JogadorInfo jogadorinfo = new JogadorInfo();
                                if (reader.GetInt32(0) == 0)
                                {
                                    resultadoComando = false;
                                }
                                else
                                {
                                    resultadoComando = true;
                                }
                            }
                        }
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }

            if (resultadoComando)
            {


                successMsg = "Pronto!";

                Response.Redirect("/Conta/Pontuacao/IndexPontuacao");

            }
            

            else
            {
                errorMsg = "Dados incorretos";
                return;
            }

    }

    }
}