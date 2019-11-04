using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class EnderecoRepository : IEndereco
    {
        public async Task<Endereco> Alterar(Endereco endereco)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(endereco).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            }
            return endereco;
        }
        

        public async Task<Endereco> BuscarPorId(int id)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                return await _contexto.Endereco.FindAsync(id);
            }
        }

        public async Task<Endereco> Excluir(Endereco endereco)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                _contexto.Endereco.Remove(endereco);
            await _contexto.SaveChangesAsync();
            return endereco;

            }
        }
        

        public async Task<List<Endereco>> Listar()
        {
            using(OrganixContext _contexto = new OrganixContext()){
                return await _contexto.Endereco.ToListAsync();
            }
        }

        public async Task<Endereco> Salvar(Endereco endereco)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(endereco);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
                return endereco;
            }
        }
        }
    }
