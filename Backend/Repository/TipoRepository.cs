using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class TipoRepository : ITipo
    {
        public async Task<Tipo> Alterar(Tipo tipo)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(tipo).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            }
            return tipo;
        }
        

        public async Task<Tipo> BuscarPorId(int id)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                return await _contexto.Tipo.FindAsync(id);
            }
        }

        public async Task<Tipo> Excluir(Tipo tipo)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                _contexto.Tipo.Remove(tipo);
            await _contexto.SaveChangesAsync();
            return tipo;

            }
        }
        

        public async Task<List<Tipo>> Listar()
        {
            using(OrganixContext _contexto = new OrganixContext()){
                return await _contexto.Tipo.ToListAsync();
            }
        }

        public async Task<Tipo> Salvar(Tipo tipo)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(tipo);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
                return tipo;
            }
        }
        }
    }
