using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace Backend.Repositories
{
    public class OfertaRepository : IOferta
    {
        public async Task<Oferta> Alterar(Oferta oferta)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(oferta).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            }
            return oferta;
        }
        

        public async Task<Oferta> BuscarPorId(int id)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                var oferta = await _contexto.Oferta.Include(o => o.IdProdutoNavigation).Include(o => o.IdUsuarioNavigation).FirstOrDefaultAsync(o=> o.IdOferta== id);
                return await _contexto.Oferta.FindAsync(id);
            }
        }

        public async Task<Oferta> Excluir(Oferta oferta)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                _contexto.Oferta.Remove(oferta);
            await _contexto.SaveChangesAsync();
            return oferta;

            }
        }
        

        public async Task<List<Oferta>> Listar()
        {
            using(OrganixContext _contexto = new OrganixContext()){
                var ofertas = await _contexto.Oferta.Include(o => o.IdProdutoNavigation).Include(o => o.IdUsuarioNavigation).ToListAsync();
                return await _contexto.Oferta.ToListAsync();
            }
        }

        public async Task<Oferta> Salvar(Oferta oferta)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(oferta);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
                return oferta;
            }
        }

        
    }
}


