using APICatalogo.Context;
using ApiUsuarios.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            //Exemplo de como tratar um erro com mais clareza para o ususario final com o TRY CATCH
            try
            {
                var usuarios = _context.Usuarios.AsNoTracking().ToList();

                if (usuarios is null)
                {
                    return NotFound("Usuarios Não Encontrados....");
                }

                return usuarios;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<Usuario> Get(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
            if (usuario is null)
            {
                return NotFound($"Usuário com id = {id} não encontrado");
            }
            return usuario;
        }

        [HttpPost]
        public ActionResult Post(Usuario usuario)
        {
            if (usuario is null)
            {
                return BadRequest();
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = usuario.UsuarioId }, usuario);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(usuario);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

            if (usuario is null)
            {
                return NotFound($"Usuário com id = {id} não encontrada");
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return Ok(usuario);
        }
        
    }
}
