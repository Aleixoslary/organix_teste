using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;

namespace Backend.Interfaces
{
    public interface IPedido
    {
         Task<List<Pedido>> Listar();

         Task<Pedido> BuscarPorId(int id);
         
         Task<Pedido> Salvar(Pedido pedido);

         Task<Pedido> Alterar(Pedido pedido);

         Task<Pedido> Excluir(Pedido pedido);
    }
}