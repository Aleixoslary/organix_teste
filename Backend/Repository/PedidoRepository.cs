using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class PedidoRepository : IPedido
    {
        public async Task<Pedido> Alterar(Pedido pedido)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(pedido).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            }
            return pedido;
        }
        

        public async Task<Pedido> BuscarPorId(int id)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                var pedido = await _contexto.Pedido.Include(p => p.ItemPedido).Include(p => p.IdUsuarioNavigation).FirstOrDefaultAsync(p => p.IdPedido == id);
                return await _contexto.Pedido.FindAsync(id);
            }
        }

        public async Task<Pedido> Excluir(Pedido pedido)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                _contexto.Pedido.Remove(pedido);
            await _contexto.SaveChangesAsync();
            return pedido;

            }
        }
        

        public async Task<List<Pedido>> Listar()
        {
            using(OrganixContext _contexto = new OrganixContext()){
                var pedidos = await _contexto.Pedido.Include(p => p.ItemPedido).Include(p => p.IdUsuarioNavigation).ToListAsync();
                return await _contexto.Pedido.ToListAsync();
            }
        }

        public async Task<Pedido> Salvar(Pedido pedido)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(pedido);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
                return pedido;
            }
        }
        }
    }
