using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    // Definimos nossa rota do controller e dizemos que é um controller de API
    [Authorize(Roles="1,2,3")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        
        UsuarioRepository _repositorio = new UsuarioRepository();
        VerificaController _verificar = new VerificaController(); 
        // GET : api/Usuario
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get(){

            var usuarios = await _repositorio.Listar();
            if(usuarios == null){
                return NotFound(new {mensagem = "Nenhum usuário foi encontrado!"});
            }
            return usuarios;
        }

        // GET : api/Usuario2
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id){

            // FindAsync = procura algo específico no banco
            var usuario = await _repositorio.BuscarPorId(id);

            if(usuario == null){
                return NotFound(new {mensagem = "Nenhum usuário foi encontrado com esse ID!"});
            }
             
            return usuario;
        }

        // POST api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario){
            try
            {
                // Tratamos contra ataques de SQL Injection
                await _repositorio.Alterar(usuario);

                usuario.CpfCnpj = usuario.CpfCnpj.Replace(".", "");
                usuario.CpfCnpj = usuario.CpfCnpj.Replace("/", ""); 
                usuario.CpfCnpj = usuario.CpfCnpj.Replace(" ", ""); 
                usuario.CpfCnpj = usuario.CpfCnpj.Replace("-", ""); 

                if (usuario.CpfCnpj.Length == 14){
                    var confirmado = _verificar.ValidaCNPJ(usuario.CpfCnpj);
                    if(confirmado == true){
                        await _repositorio.Salvar(usuario);
                    }else{
                        return BadRequest(new {mensagem = "Cnpj inválido!"});
                    }         
                              
                } else if(usuario.CpfCnpj.Length == 11){
                    var confirmado =  _verificar.ValidaCPF(usuario.CpfCnpj);
                    if(confirmado == true){
                        await _repositorio.Salvar(usuario);
                    }else{
                        return BadRequest(new {mensagem = "Cpf inválido!"});
                    }      
                }else{
                   return BadRequest(new {mensagem = "CPF ou CNPJ inválido, verifique os dados cadastrados!"});
                }
            }
            catch (DbUpdateConcurrencyException){  
                throw;
            }
            return usuario;
        }

        // private bool ValidaCPF()
        // {
        //     throw new NotImplementedException();
        // }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Usuario usuario){
            // Se o id do objeto não existir, ele retorna erro 400
            if(id != usuario.IdUsuario){
                return BadRequest(new {mensagem = "Nenhum usuário cadastrado com este id!"});
            }
                usuario.CpfCnpj = usuario.CpfCnpj.Replace(".", "");
                usuario.CpfCnpj = usuario.CpfCnpj.Replace("/", ""); 
                usuario.CpfCnpj = usuario.CpfCnpj.Replace(" ", ""); 
                usuario.CpfCnpj = usuario.CpfCnpj.Replace("-", ""); 

                if (usuario.CpfCnpj.Length == 14){
                    var confirmado = _verificar.ValidaCNPJ(usuario.CpfCnpj);
                    if(confirmado == true){
                        await _repositorio.Salvar(usuario);
                    }else{
                        return BadRequest(new {mensagem = "Cnpj inválido!"});
                    }         
                              
                } else if(usuario.CpfCnpj.Length == 11){
                    var confirmado =  _verificar.ValidaCPF(usuario.CpfCnpj);
                    if(confirmado == true){
                        await _repositorio.Salvar(usuario);
                    }else{
                        return BadRequest(new {mensagem = "Cpf inválido!"});
                    }      
                }else{
                   return BadRequest(new {mensagem = "CPF ou CNPJ inválido, verifique os dados cadastrados!"});
                }
            
            // Comparamos os atributos que foram modificados através do EF
            try
            {
                await _repositorio.Alterar(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verificamos se o objeto inserido realmente existe no banco
                var usuario_valido = await _repositorio.BuscarPorId(id);

                if(usuario_valido == null){
                    return NotFound(new {mensagem = "Nenhum usuário encontrado!"});
                }else{

                throw;
                }

                
            }
            // NoContent = retorna 204, sem nada
            return NoContent();
        }

        // DELETE api/usuario/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id){
            var usuario = await _repositorio.BuscarPorId(id);
            if(usuario == null){
                return NotFound(new {mensagem = "Não foi possível deletar o usuário pois o ID não existe!"});
            }
        
            await _repositorio.Excluir(usuario);

            return usuario;
        }

    }
}