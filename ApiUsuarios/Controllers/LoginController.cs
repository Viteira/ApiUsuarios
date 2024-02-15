using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ApiUsuarios.Models;
using APICatalogo.Context;

namespace ApiUsuarios.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("/login")]
        public ActionResult<Usuario> Login([FromBody] Login user)
        {

            try
            {
                var usuario = _context.Usuarios.Where(u => u.Email == user.Email && u.Senha == user.Senha).FirstOrDefault();
                if (usuario is null)
                {
                    return NotFound("Usuário não encontrado ou senha inválida");
                }
                return usuario;
            }
            catch
            {
                return NotFound("Usuário não encontrado");
            }
        }
    }
}
