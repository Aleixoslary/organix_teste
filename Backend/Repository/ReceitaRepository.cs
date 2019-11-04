using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Backend.Repositories
{
    public class ReceitaRepository : IReceita
    {
        public async Task<Receita> Alterar(Receita receita)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(receita).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            }
            return receita;
        }
        

        public async Task<Receita> BuscarPorId(int id)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                var receita = await _contexto.Receita.Include(r => r.IdUsuarioNavigation).Include(r=> r.IdCategoriaReceitaNavigation).FirstOrDefaultAsync(r=> r.IdReceita== id);
                return await _contexto.Receita.FindAsync(id);
            }
        }

        public async Task<Receita> Excluir(Receita receita)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                _contexto.Receita.Remove(receita);
            await _contexto.SaveChangesAsync();
            return receita;

            }
        }
        

        public async Task<List<Receita>> Listar()
        {
            using(OrganixContext _contexto = new OrganixContext()){
                var receitas = await _contexto.Receita.Include(r => r.IdUsuarioNavigation).Include(r=> r.IdCategoriaReceitaNavigation).
            ToListAsync();
                return await _contexto.Receita.ToListAsync();
            }
        }

        public async Task<Receita> Salvar(Receita receita)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(receita);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
                
            }
            return receita;
        }
        }
    }

