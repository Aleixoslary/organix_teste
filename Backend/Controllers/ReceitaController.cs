using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Repositories;
using Backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    // Definimos nossa rota do controller e dizemos que é um controller de API
    [Authorize(Roles="1,3")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        // GufosContext _contexto = new GufosContext();

        ReceitaRepository _repositorio = new ReceitaRepository();

        // GET : api/Receita
        [HttpGet]
        public async Task<ActionResult<List<Receita>>> Get(){

            var receitas = await _repositorio.Listar();

            if(receitas == null){
                return NotFound();
            }

            return receitas;

        }

        // GET : api/Receita2
        [HttpGet("{id}")]
        public async Task<ActionResult<Receita>> Get(int id){

            // FindAsync = procura algo específico no banco
            var receita = await _repositorio.BuscarPorId(id);

            if(receita == null){
                return NotFound();
            }

            return receita;

        }
    [HttpGet ("FiltrarPorNome")]
        public async Task<ActionResult<List<Receita>>> GetFiltro (FiltroViewModel filtro) {

                using (OrganixContext _context = new OrganixContext ()){

                List<Receita> receita = await _context.Receita.Where (c=> c.NomeReceita.Contains (filtro.Filtro)).ToListAsync();
                return receita;
                }
        }

        // POST api/Receita
        [HttpPost]
        public async Task<ActionResult<Receita>> Post([FromForm]Receita receita){

            try
            {
             var file = Request.Form.Files[0];
                var folderName = Path.Combine ("Resources", "Images");
                var pathToSave = Path.Combine (Directory.GetCurrentDirectory (), folderName);

                if (file.Length > 0) {
                    var fileName = ContentDispositionHeaderValue.Parse (file.ContentDisposition).FileName.Trim ('"');
                    var fullPath = Path.Combine (pathToSave, fileName);
                    var dbPath = Path.Combine (folderName, fileName);

                    using (var stream = new FileStream (fullPath, FileMode.Create)) {
                        file.CopyTo (stream);
                    }

                    receita.Imagem =  fileName;   
                await _repositorio.Salvar(receita);
            }
            }
            catch (DbUpdateConcurrencyException)
            {
                
                throw;
            }

            return receita;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Receita receita){
            // Se o id do objeto não existir, ele retorna erro 400
            if(id != receita.IdReceita){
                return BadRequest();
            }
            
            

            try
            {

                await _repositorio.Alterar(receita);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verificamos se o objeto inserido realmente existe no banco
                var receita_valido = await _repositorio.BuscarPorId(id);

                if(receita_valido == null){
                    return NotFound();
                }else{

                throw;
                }

                
            }
            // NoContent = retorna 204, sem nada
            return NoContent();
        }

        // DELETE api/receita/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Receita>> Delete(int id){
            var receita = await _repositorio.BuscarPorId(id);
            if(receita == null){
                return NotFound();
            }
            await _repositorio.Excluir(receita);
            
            return receita;
        }
    }
}