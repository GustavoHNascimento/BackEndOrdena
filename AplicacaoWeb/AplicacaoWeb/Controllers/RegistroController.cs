using AplicacaoWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace AplicacaoWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RegistroController(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        [HttpPost]
        [Route("registro")]
        public string registro(Registro registro)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Ordena").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Registro(Nome,Senha,Email) VALUES ('" + registro.Nome + " ',' " + registro.Senha + " ',' " + registro.Email + "')", con);
            con.Open(); 
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if(i > 0)
            {
                return "registrado com Sucesso";
            }
            else 
            {
                return "Erro ao inserir dados"; 
            }
        }

        [HttpPost]
        [Route("login")]
        public string Login(Registro registro)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Ordena").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Registro WHERE Email = '" +registro.Email+"' AND Senha = '" +registro.Senha+"'" ,con);
            
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                return "Registro Encontrado";
            }
            else
            {
                return "Erro  encontrar usuario";
            }
        }
    }

    
}
