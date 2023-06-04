using Api.Auth;
using Dominio.Entidades;
using Dominio.Servicos;
using Dominio.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaPetsBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        public ApplicationDbContext _context { get; set; }
        public ServicoUsuario _servicoUsuario{ get; set; }
    
        public UsuarioController(ApplicationDbContext context, ServicoUsuario servicoUsuario)
        {
            _context = context;
            _servicoUsuario = servicoUsuario;
        }

        [HttpPost]
        [Autorizar]
        public IActionResult Post(Usuario usuario)
        {
            try
            {
                var result = _servicoUsuario.Inclui(usuario);
                return result != null ? StatusCode(200, result) : StatusCode(422);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                var result = _context.Usuario.Where(x => x.Id == id).FirstOrDefault();
                return StatusCode(200, result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginViewModel login)
        {
            try
            {
                var retorno = _servicoUsuario.Login(login);
                return retorno != null ? StatusCode(200, retorno) : StatusCode(401);    
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
