using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class TelefoneRepository : ITelefone
    {
        public async Task<Telefone> Alterar(Telefone telefone)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(telefone).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            }
            return telefone;
        }
        

        public async Task<Telefone> BuscarPorId(int id)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                return await _contexto.Telefone.FindAsync(id);
            }
        }

        public async Task<Telefone> Excluir(Telefone telefone)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                _contexto.Telefone.Remove(telefone);
            await _contexto.SaveChangesAsync();
            return telefone;

            }
        }
        

        public async Task<List<Telefone>> Listar()
        {
            using(OrganixContext _contexto = new OrganixContext()){
                return await _contexto.Telefone.ToListAsync();
            }
        }

        public async Task<Telefone> Salvar(Telefone telefone)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(telefone);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
                return telefone;
            }
        }
        }
    }
