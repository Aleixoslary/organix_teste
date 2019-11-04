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
    public class EnderecoController : ControllerBase
    {
        // GufosContext _contexto = new GufosContext();

        EnderecoRepository _repositorio = new EnderecoRepository();

        // GET : api/Endereco
        [HttpGet]
        public async Task<ActionResult<List<Endereco>>> Get(){

            var enderecos = await _repositorio.Listar();

            if(enderecos == null){
                return NotFound();
            }

            return enderecos;

        }

        // GET : api/Endereco2
        [HttpGet("{id}")]
        public async Task<ActionResult<Endereco>> Get(int id){

            // FindAsync = procura algo específico no banco
            var endereco = await _repositorio.BuscarPorId(id);

            if(endereco == null){
                return NotFound();
            }

            return endereco;

        }

        // POST api/Endereco
        [HttpPost]
        public async Task<ActionResult<Endereco>> Post(Endereco endereco){

            try
            {
                await _repositorio.Salvar(endereco);
            }
            catch (DbUpdateConcurrencyException)
            {
                
                throw;
            }

            return endereco;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Endereco endereco){
            // Se o id do objeto não existir, ele retorna erro 400
            if(id != endereco.IdEndereco){
                return BadRequest();
            }
            
            

            try
            {

                await _repositorio.Alterar(endereco);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verificamos se o objeto inserido realmente existe no banco
                var endereco_valido = await _repositorio.BuscarPorId(id);

                if(endereco_valido == null){
                    return NotFound();
                }else{

                throw;
                }

                
            }
            // NoContent = retorna 204, sem nada
            return NoContent();
        }

        // DELETE api/endereco/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Endereco>> Delete(int id){
            var endereco = await _repositorio.BuscarPorId(id);
            if(endereco == null){
                return NotFound();
            }
            await _repositorio.Excluir(endereco);
            
            return endereco;
        }
    }
}