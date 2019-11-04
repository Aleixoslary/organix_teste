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
    public class TipoController : ControllerBase
    {
        // GufosContext _contexto = new GufosContext();

        TipoRepository _repositorio = new TipoRepository();

        // GET : api/Tipo
        [HttpGet]
        public async Task<ActionResult<List<Tipo>>> Get(){

            var tipos = await _repositorio.Listar();

            if(tipos == null){
                return NotFound();
            }

            return tipos;

        }

        // GET : api/Tipo2
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo>> Get(int id){

            // FindAsync = procura algo específico no banco
            var tipo = await _repositorio.BuscarPorId(id);

            if(tipo == null){
                return NotFound();
            }

            return tipo;

        }

        // POST api/Tipo
        [HttpPost]
        public async Task<ActionResult<Tipo>> Post(Tipo tipo){

            try
            {
                await _repositorio.Salvar(tipo);
            }
            catch (DbUpdateConcurrencyException)
            {
                
                throw;
            }

            return tipo;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Tipo tipo){
            // Se o id do objeto não existir, ele retorna erro 400
            if(id != tipo.IdTipo){
                return BadRequest();
            }
            
            

            try
            {

                await _repositorio.Alterar(tipo);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verificamos se o objeto inserido realmente existe no banco
                var tipo_valido = await _repositorio.BuscarPorId(id);

                if(tipo_valido == null){
                    return NotFound();
                }else{

                throw;
                }

                
            }
            // NoContent = retorna 204, sem nada
            return NoContent();
        }

        // DELETE api/tipo/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tipo>> Delete(int id){
            var tipo = await _repositorio.BuscarPorId(id);
            if(tipo == null){
                return NotFound();
            }
            await _repositorio.Excluir(tipo);
            
            return tipo;
        }
    }
}