using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class ItemPedidoRepository : IItemPedido
    {
        public async Task<ItemPedido> Alterar(ItemPedido itemPedido)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(itemPedido).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            }
            return itemPedido;
        }
        

        public async Task<ItemPedido> BuscarPorId(int id)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                var itemPedido = await _contexto.ItemPedido.Include(i => i.IdOfertaNavigation).Include(i =>i.IdPedidoNavigation).FirstOrDefaultAsync(i=> i.IdItemPedido == id);
                return await _contexto.ItemPedido.FindAsync(id);
            }
        }

        public async Task<ItemPedido> Excluir(ItemPedido itemPedido)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                _contexto.ItemPedido.Remove(itemPedido);
            await _contexto.SaveChangesAsync();
            return itemPedido;

            }
        }
        

        public async Task<List<ItemPedido>> Listar()
        {
            using(OrganixContext _contexto = new OrganixContext()){
                var itemPedidos = await _contexto.ItemPedido.Include(i => i.IdOfertaNavigation).Include(i =>i.IdPedidoNavigation).ToListAsync();
                return await _contexto.ItemPedido.ToListAsync();
                
            }
        }

        public async Task<ItemPedido> Salvar(ItemPedido itemPedido)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(itemPedido);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
                return itemPedido;
            }
        }
        }
    }
