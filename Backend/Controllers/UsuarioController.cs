using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
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
        OrganixContext _contexto = new OrganixContext();
        // GET : api/Usuario
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get(){

            var usuarios = await _contexto.Usuario.Include(u => u.Endereco).Include(u => u.Telefone).
            Include(u => u.IdTipoNavigation).ToListAsync();
            if(usuarios == null){
                return NotFound();
            }
            return usuarios;
        }

        // GET : api/Usuario2
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id){

            // FindAsync = procura algo específico no banco
            var usuario = await _contexto.Usuario.Include(u => u.Endereco).Include(u => u.Telefone).
            Include(u => u.IdTipoNavigation).FirstOrDefaultAsync(e => e.IdUsuario == id);

            if(usuario == null){
                return NotFound();
            }

            return usuario;

        }

        // POST api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario){
            try
            {
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(usuario);
                if (ValidaCPF(usuario.CpfCnpj)==true && ValidaCNPJ(usuario.CpfCnpj)==true){
                    // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
                } else{
                   return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                
                throw;
            }

            return usuario;
        }

        private bool ValidaCPF()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Usuario usuario){
            // Se o id do objeto não existir, ele retorna erro 400
            if(id != usuario.IdUsuario){
                return BadRequest();
            }
            
            // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verificamos se o objeto inserido realmente existe no banco
                var usuario_valido = await _contexto.Usuario.FindAsync(id);

                if(usuario_valido == null){
                    return NotFound();
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
            var usuario = await _contexto.Usuario.FindAsync(id);
            if(usuario == null){
                return NotFound();
            }
            _contexto.Usuario.Remove(usuario);
            await _contexto.SaveChangesAsync();

            return usuario;
        }

        static bool ValidaCPF(string cpfUsuario){

            bool resultado = false;
            int[] v1 ={10,9,8,7,6,5,4,3,2};
            string cpfCalculo ="";
            int resto=0;
            string digito_v1="";
            string digito_v2="";
            int calculo=0;

            cpfCalculo = cpfUsuario.Substring(0,9);

            for(int i=0; i<=8;i++){
                calculo+= int.Parse(cpfCalculo[i].ToString()) * v1[i];
            }

           resto=calculo%11;
           calculo=11-resto;

            if(calculo>9){
                digito_v1="0";
            } else{
                digito_v1=calculo.ToString();
            }

            if(digito_v1 ==cpfUsuario[9].ToString()){
                resultado=true;
            }

            int[] v2={11,10,9,8,7,6,5,4,3,2};
            resto=0;

            cpfCalculo= cpfCalculo+calculo.ToString();
            calculo=0;

            for(int i=0; i<=9;i++){
                calculo+= int.Parse(cpfCalculo[i].ToString()) * v2[i];
            }

           resto=calculo%11;
           calculo=11-resto;

            if(calculo>9){
                digito_v2="0";
            } else{
                digito_v2=calculo.ToString();
            }

            if(digito_v2==cpfUsuario[10].ToString()){
                resultado=true;
            }else{
                resultado=false;
            }
            return resultado;
        }

           static bool ValidaCNPJ(string cnpj){

            bool resultado=false;
            int[] v1={5,4,3,2,9,8,7,6,5,4,3,2};
            string cnpjCalc="";
            int resto=0;
            string di_v1="";
            string di_v2 ="";
            int calc=0;

            cnpj= cnpj.Replace(" ","");
            cnpj=cnpj.Replace("-","");
            cnpj=cnpj.Replace(".","");
            cnpj=cnpj.Replace("/","");

            cnpjCalc=cnpj.Substring(0,12);

            for (int i=0; i<=11;i++){
                calc += int.Parse(cnpjCalc[i].ToString())*v1[i];
            }

            resto= calc%11;
            calc=11-resto;

            if(resto<2){
                di_v1="0";
            }else{
                di_v1=calc.ToString();
            }

            if(di_v1 ==cnpj[12].ToString()){
                resultado=true;
            }

            int[] v2={6,5,4,3,2,9,8,7,6,5,4,3,2};
            resto=0;
            cnpjCalc= cnpjCalc+calc.ToString();
            calc=0;
            
            for (int i=0; i<=12;i++){
                calc += int.Parse(cnpjCalc[i].ToString())*v2[i];
            }

            resto= calc%11;
            calc=11-resto;

            if(resto<2){
                di_v2="0";
            }else{
                di_v2=calc.ToString();
            }

            if(di_v2 ==cnpj[13].ToString()){
                resultado=true;

            }else{ resultado=false;
            }

            return resultado;
        }
    }
}