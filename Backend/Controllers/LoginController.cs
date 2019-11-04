using Microsoft.AspNetCore.Mvc;
using Backend.Controllers;
using Backend.Domains;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]  
    [ApiController] 
    public class LoginController : ControllerBase
    {
        // Chamamos nosso contexto do banco
        OrganixContext _context = new OrganixContext();

        private IConfiguration _config;
   // Definimos um método construtor para poder passar essas configs
        public LoginController(IConfiguration config)  
        {  
            _config = config;  
        }

        // Chamamos nosso método para validar nosso usuário da aplicação
        private Usuario AuthenticateUser(Usuario login)  
        {  
            var usuario =  _context.Usuario.Include( l => l.IdTipoNavigation).FirstOrDefault(u => u.Email == login.Email && u.Senha == login.Senha);
  
            if (usuario != null)  
            {  
                // usuario = login;  
            }  

            return usuario;  
        }  

        // Criamos nosso método que vai gerar nosso Token
        private string GenerateJSONWebToken(Usuario userInfo)  
        {  
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));  
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Definimos nossas Claims (dados da sessão) para poderem ser capturadas
            // a qualquer momento enquanto o Token for ativo
            var claims = new[] {  
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.Nome),  
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(ClaimTypes.Role,  userInfo.IdTipo.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  
            }; 

            // Configuramos nosso Token e seu tempo de vida
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],  
              _config["Jwt:Issuer"],  
              claims,  
              expires: DateTime.Now.AddMinutes(120),  
              signingCredentials: credentials);  
  
            return new JwtSecurityTokenHandler().WriteToken(token);  
        }  
  

        
        // Usamos essa anotação para ignorar a autenticação neste método, já que é ele quem fará isso  
        [AllowAnonymous]  
        [HttpPost]  
        public IActionResult Login([FromBody]Usuario login)  
        {  
            IActionResult response = Unauthorized();  
            var user = AuthenticateUser(login);  
  
            if (user != null)  
            {  
                var tokenString = GenerateJSONWebToken(user);  
                response = Ok(new { token = tokenString });  
            }  
  
            return response;  
        }

    }
}