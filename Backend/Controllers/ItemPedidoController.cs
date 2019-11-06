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
    public class ItemPedidoController : ControllerBase
    {
        // organixContext _contexto = new organixContext();

        ItemPedidoRepository _repositorio = new ItemPedidoRepository();

        // GET : api/ItemPedido
        [HttpGet]
        public async Task<ActionResult<List<ItemPedido>>> Get(){

            var itemPedidos = await _repositorio.Listar();

            if(itemPedidos == null){
                return NotFound(new {mensagem = "Não foi possível deletar o produto pois o ID informado não existe!"});
            }

            return itemPedidos;

        }

        // GET : api/ItemPedido2
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemPedido>> Get(int id){

            // FindAsync = procura algo específico no banco
            var itemPedido = await _repositorio.BuscarPorId(id);

            if(itemPedido == null){
                return NotFound();
            }

            return itemPedido;

        }

        // POST api/ItemPedido
        [HttpPost]
        public async Task<ActionResult<ItemPedido>> Post(ItemPedido itemPedido){

            try
            {
                await _repositorio.Salvar(itemPedido);
            }
            catch (DbUpdateConcurrencyException)
            {
                
                throw;
            }

            return itemPedido;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ItemPedido itemPedido){
            // Se o id do objeto não existir, ele retorna erro 400
            if(id != itemPedido.IdItemPedido){
                return BadRequest();
            }
            
            

            try
            {

                await _repositorio.Alterar(itemPedido);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verificamos se o objeto inserido realmente existe no banco
                var itemPedido_valido = await _repositorio.BuscarPorId(id);

                if(itemPedido_valido == null){
                    return NotFound();
                }else{

                throw;
                }

                
            }
            // NoContent = retorna 204, sem nada
            return NoContent();
        }

        // DELETE api/itemPedido/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemPedido>> Delete(int id){
            var itemPedido = await _repositorio.BuscarPorId(id);
            if(itemPedido == null){
                return NotFound();
            }
            await _repositorio.Excluir(itemPedido);
            
            return itemPedido;
        }
    }
}