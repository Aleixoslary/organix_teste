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
    public class TelefoneController : ControllerBase
    {
        // organixContext _contexto = new organixContext();

        TelefoneRepository _repositorio = new TelefoneRepository();

        // GET : api/Telefone
        [HttpGet]
        public async Task<ActionResult<List<Telefone>>> Get(){

            var telefones = await _repositorio.Listar();

            if(telefones == null){
                return NotFound(new {mensagem = "Nenhum telefone foi encontrado!"});
            }

            return telefones;

        }

        // GET : api/Telefone2
        [HttpGet("{id}")]
        public async Task<ActionResult<Telefone>> Get(int id){

            // FindAsync = procura algo específico no banco
            var telefone = await _repositorio.BuscarPorId(id);

            if(telefone == null){
               return NotFound(new {mensagem = "Nenhum telefone foi encontrado para o ID informado verifique e tente novamente!"});
            }

            return telefone;

        }

        // POST api/Telefone
        [HttpPost]
        public async Task<ActionResult<Telefone>> Post(Telefone telefone){

            try
            {
                await _repositorio.Salvar(telefone);
            }
            catch (DbUpdateConcurrencyException)
            {
                
                throw;
            }

            return telefone;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Telefone telefone){
            // Se o id do objeto não existir, ele retorna erro 400
            if(id != telefone.IdTelefone){
                return NotFound(new {mensagem = "Nenhum telefone foi encontrado para o ID informado verifique e tente novamente!"});
            }
            
            

            try
            {

                await _repositorio.Alterar(telefone);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verificamos se o objeto inserido realmente existe no banco
                var telefone_valido = await _repositorio.BuscarPorId(id);

                if(telefone_valido == null){
                    return NotFound(new {mensagem = "Nada foi encontrado!"});
                }else{

                throw;
                }

                
            }
            // NoContent = retorna 204, sem nada
            return NoContent();
        }

        // DELETE api/telefone/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Telefone>> Delete(int id){
            var telefone = await _repositorio.BuscarPorId(id);
            if(telefone == null){
                return NotFound(new {mensagem = "Não foi possível deletar pois nenhum telefone foi encontrado para o ID informado verifique e tente novamente!"});
            }
            await _repositorio.Excluir(telefone);
            
            return telefone;
        }
    }
}