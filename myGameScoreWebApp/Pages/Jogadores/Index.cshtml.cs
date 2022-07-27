using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace myGameScoreWebApp.Pages.Jogadores
{
    public class IndexModel : PageModel
    {
        public List<JogadorInfo> listJogadores = new List<JogadorInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source =.; Initial Catalog = myGameScore; Integrated Security = True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM TBUSER";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                JogadorInfo jogadorinfo = new JogadorInfo(); 
                                jogadorinfo.id_user = "" + reader.GetInt32(0);
                                jogadorinfo.nome_user = reader.GetString(1);
                                jogadorinfo.email_user = reader.GetString(2);

                                listJogadores.Add(jogadorinfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { 
            }
        }
    }
    public class JogadorInfo
    {
        public string id_user;
        public string nome_user;
        public string email_user;
        
    }
}
