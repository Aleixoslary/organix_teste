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

    public class UsuarioRepository : IUsuario
    {
        // Usuario user = new Usuario();
        public async Task<Usuario> Alterar(Usuario usuario)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(usuario).State = EntityState.Modified;
            
            await _contexto.SaveChangesAsync();
            }
            return usuario;
        }
        

        public async Task<Usuario> BuscarPorId(int id)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                var user = await _contexto.Usuario.Include(u => u.Endereco).Include(u => u.Telefone).
            Include(u => u.IdTipoNavigation).FirstOrDefaultAsync(e => e.IdUsuario == id);
                user.Email = null;
                user.Senha = null;
                return user;
            }
        }

        public async Task<Usuario> Excluir(Usuario usuario)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                _contexto.Usuario.Remove(usuario);
            await _contexto.SaveChangesAsync();
            return usuario;

            }
        }
        

        public async Task<List<Usuario>> Listar()
        {
            using(OrganixContext _contexto = new OrganixContext()){
                List<Usuario> user =  new List<Usuario>(await _contexto.Usuario.Include(u => u.Endereco).Include(u => u.Telefone).
            Include(u => u.IdTipoNavigation).ToListAsync());
                foreach (Usuario usuario in user)
                {
                    usuario.Email = null;
                    usuario.Senha = null;
                }
                return user; 
            }
        }

        public async Task<Usuario> Salvar(Usuario usuario)
        {
            using(OrganixContext _contexto = new OrganixContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(usuario);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
                      
            }
            return usuario;
        }
        }


        
    }

