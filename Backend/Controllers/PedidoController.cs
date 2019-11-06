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
    public class PedidoController : ControllerBase
    {
        // organixContext _contexto = new organixContext();

        PedidoRepository _repositorio = new PedidoRepository();

        // GET : api/Pedido
        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> Get(){

            var pedidos = await _repositorio.Listar();

            if(pedidos == null){
                return NotFound(new {mensagem = "Nenhum pedido foi encontrado!"});;
            }

            return pedidos;

        }

        // GET : api/Pedido2
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> Get(int id){

            // FindAsync = procura algo específico no banco
            var pedido = await _repositorio.BuscarPorId(id);

            if(pedido == null){
                return NotFound(new {mensagem = "Nenhum produto foi encontrado para o id informado!"});
            }

            return pedido;

        }

        // POST api/Pedido
        [HttpPost]
        public async Task<ActionResult<Pedido>> Post(Pedido pedido){

            try
            {
                await _repositorio.Salvar(pedido);
            }
            catch (DbUpdateConcurrencyException)
            {
                
                throw;
            }

            return pedido;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Pedido pedido){
            // Se o id do objeto não existir, ele retorna erro 400
            if(id != pedido.IdPedido){
                return BadRequest();
            }
            
            

            try
            {

                await _repositorio.Alterar(pedido);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verificamos se o objeto inserido realmente existe no banco
                var pedido_valido = await _repositorio.BuscarPorId(id);

                if(pedido_valido == null){
                    return NotFound(new {mensagem = "Nenhum produto foi encontrado!"});
                }else{

                throw;
                }

                
            }
            // NoContent = retorna 204, sem nada
            return NoContent();
        }

        // DELETE api/pedido/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pedido>> Delete(int id){
            var pedido = await _repositorio.BuscarPorId(id);
            if(pedido == null){
                return NotFound(new {mensagem = "Não foi possível deletar o produto pois o ID informado não existe!"});
            }
            await _repositorio.Excluir(pedido);
            
            return pedido;
        }
    }
}