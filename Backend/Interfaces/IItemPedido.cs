using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;

namespace Backend.Interfaces
{
    public interface IItemPedido
    {
         Task<List<ItemPedido>> Listar();

         Task<ItemPedido> BuscarPorId(int id);
         
         Task<ItemPedido> Salvar(ItemPedido itemPedido);

         Task<ItemPedido> Alterar(ItemPedido itemPedido);

         Task<ItemPedido> Excluir(ItemPedido itemPedido);
    }
}