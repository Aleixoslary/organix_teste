using System.Threading.Tasks;
using Backend.Domains;
// using Microsoft.Data.SqlClient;
// using System.Linq;
// using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Backend.Interfaces;
using Microsoft.Data.SqlClient;
using Backend.ViewModels;

namespace Backend.Repository
{
    public class FiltroRepository : IFiltro
    {
          public async Task<List<Oferta>> Filtro(FiltroViewModel Dados)
        {
           using(OrganixContext _contexto = new OrganixContext()){

            
            var produtoQuery = new SqlParameter("@produto",Dados.produto);
            var regiaoQuery = new SqlParameter("@regiao", Dados.regiao);
            var menorPrecoQuery = new SqlParameter("@menorPreco",Dados.menorPreco);
            var maiorPrecoQuery = new SqlParameter("@maiorPreco",Dados.maiorPreco);

               var lista = await _contexto.Oferta.FromSqlRaw("select usuario.id_usuario, endereco.regiao, oferta.id_produto, oferta.id_oferta, oferta.preco,oferta.data_fabricacao, oferta.data_vencimento, oferta.estado_produto from oferta inner join usuario  on usuario.id_usuario = oferta.id_usuario inner join endereco on usuario.id_usuario = endereco.id_usuario where oferta.id_produto= @produto and oferta.preco <= @maiorPreco and oferta.preco>= @menorPreco and endereco.regiao= @regiao",produtoQuery,menorPrecoQuery,maiorPrecoQuery,regiaoQuery).ToListAsync();

               return  lista;
                }
        }
    }
}

    //         return produto;

 // string connectionString = @"Data Source =DESKTOP-9J1BUVT\\SQLEXPRESS; Database=Organix; User Id=sa; Password=132";
            // // using (SqlConnection connection = new SqlConnection(connectionString)){

            //   
            //     // string FiltroQuery = ;

                // SqlCommand command = new SqlCommand(connectionString, FiltroQuery);


            // SqlConnection cnn;
            // 

            // cnn=new SqlConnection(connectionString);

            // cnn.Open();


               // SqlConnection conn = new SqlConnection("Server.DESKTOP-9J1BUVT\\SQLEXPRESS; Database=Organix; User Id=sa; Password=132");
            // conn.Open();
            //   SqlCommand cmd = new SqlCommand("select * From Oferta");
            //   SqlDataReader reader = cmd.ExecuteReader();
              
            // return oferta;


