using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositories

{

    public class ProdutoRepository : IProduto
    {
        public async Task<Produto> Alterar(Produto produto)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(produto).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            }
            return produto;
        }
        

        public async Task<Produto> BuscarPorId(int id)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                return await _contexto.Produto.FindAsync(id);
            }
        }

        public async Task<Produto> Excluir(Produto produto)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                _contexto.Produto.Remove(produto);
            await _contexto.SaveChangesAsync();
            return produto;

            }
        }
        

        public async Task<List<Produto>> Listar()
        {
            using(OrganixContext _contexto = new OrganixContext()){
                return await _contexto.Produto.ToListAsync();
            }
        }

        public async Task<Produto> Salvar(Produto produto)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(produto);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
                 
                
                
            }
            return produto;
        }
        }
    }

