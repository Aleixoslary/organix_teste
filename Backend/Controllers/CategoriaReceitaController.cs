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
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaReceitaController : ControllerBase
    {
        // GufosContext _contexto = new GufosContext();

        CategoriaReceitaRepository _repositorio = new CategoriaReceitaRepository();

        // GET : api/CategoriaReceita
        [Authorize(Roles="1, 3")]
        [HttpGet]
        public async Task<ActionResult<List<CategoriaReceita>>> Get(){

            var categoriaReceitas = await _repositorio.Listar();

            if(categoriaReceitas == null){
                return NotFound();
            }

            return categoriaReceitas;

        }

        // GET : api/CategoriaReceita2
        [Authorize(Roles="1, 3")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaReceita>> Get(int id){

            // FindAsync = procura algo específico no banco
            var categoriaReceita = await _repositorio.BuscarPorId(id);

            if(categoriaReceita == null){
                return NotFound();
            }

            return categoriaReceita;

        }

        // POST api/CategoriaReceita
        [Authorize(Roles="1")]
        [HttpPost]
        public async Task<ActionResult<CategoriaReceita>> Post(CategoriaReceita categoriaReceita){

            try
            {
                await _repositorio.Salvar(categoriaReceita);
            }
            catch (DbUpdateConcurrencyException)
            {
                
                throw;
            }

            return categoriaReceita;
        }
        [Authorize(Roles="1")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CategoriaReceita categoriaReceita){
            // Se o id do objeto não existir, ele retorna erro 400
            if(id != categoriaReceita.IdCategoriaReceita){
                return BadRequest();
            }
            
            

            try
            {

                await _repositorio.Alterar(categoriaReceita);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verificamos se o objeto inserido realmente existe no banco
                var categoriaReceita_valido = await _repositorio.BuscarPorId(id);

                if(categoriaReceita_valido == null){
                    return NotFound();
                }else{

                throw;
                }

                
            }
            // NoContent = retorna 204, sem nada
            return NoContent();
        }

        // DELETE api/categoriaReceita/id
        [Authorize(Roles="1")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaReceita>> Delete(int id){
            var categoriaReceita = await _repositorio.BuscarPorId(id);
            if(categoriaReceita == null){
                return NotFound();
            }
            await _repositorio.Excluir(categoriaReceita);
            
            return categoriaReceita;
        }
    }
}