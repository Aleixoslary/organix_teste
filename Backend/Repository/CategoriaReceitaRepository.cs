using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class CategoriaReceitaRepository : ICategoriaReceita
    {
        public async Task<CategoriaReceita> Alterar(CategoriaReceita categoriaReceita)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(categoriaReceita).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            }
            return categoriaReceita;
        }
        

        public async Task<CategoriaReceita> BuscarPorId(int id)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                return await _contexto.CategoriaReceita.FindAsync(id);
            }
        }

        public async Task<CategoriaReceita> Excluir(CategoriaReceita categoriaReceita)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                _contexto.CategoriaReceita.Remove(categoriaReceita);
            await _contexto.SaveChangesAsync();
            return categoriaReceita;

            }
        }
        

        public async Task<List<CategoriaReceita>> Listar()
        {
            using(OrganixContext _contexto = new OrganixContext()){
                return await _contexto.CategoriaReceita.ToListAsync();
            }
        }

        public async Task<CategoriaReceita> Salvar(CategoriaReceita categoriaReceita)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(categoriaReceita);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
                return categoriaReceita;
            }
        }
        }
    }
