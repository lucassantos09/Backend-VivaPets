using Dominio.Entidades;
using Dominio.ViewModel;
using Jose;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;


namespace Dominio.Servicos
{
    public class ServicoUsuario
    {
        public ApplicationDbContext _context { get; set; }
        public ServicoToken _servicoToken { get; set; }

        public ServicoUsuario(ApplicationDbContext context, ServicoToken servicoToken)
        {
            _context = context;
            _servicoToken = servicoToken;
        }

        public RetornoLoginViewModel Login(LoginViewModel login)
        {
            var usuario = _context.Usuario.Where(a => a.Email == login.Usuario).FirstOrDefault();
            if (usuario == null)
            {
                return null;
            }

            var isSenhaValida = BCryptNet.Verify(login.Senha, usuario.Senha);

            if (!isSenhaValida)
            {
                return null;
            }

            var token = _servicoToken.GerarToken(usuario.Id.ToString());
            var retorno = new RetornoLoginViewModel
            {
                Token = token
            };

            return retorno;
        }

        public Usuario Inclui(Usuario usuario)
        {
            try
            {
                var user = _context.Usuario.Where(a => a.Email == usuario.Email).FirstOrDefault();
                if (user != null)
                {
                    return null;
                }
                var usuarioInclui = usuario;
                usuarioInclui.Senha = BCryptNet.HashPassword(usuario.Senha);

                var retorno = _context.Add(usuarioInclui).Entity;
                _context.SaveChanges();

                return retorno;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
