using Dominio.ViewModel;
using Jose;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Servicos
{
    public class ServicoUsuario
    {
        public ApplicationDbContext _context { get; set; }
        public ServicoUsuario(ApplicationDbContext context)
        {
            _context = context;
        }

        public RetornoLoginViewModel Login(LoginViewModel login)
        {
            var usuario = _context.Usuario.Where(a => a.Email == login.Usuario && a.Senha == login.Senha).FirstOrDefault();
            if (usuario == null)
            {
                return null;
            }

            var payload = new Dictionary<string, object>
                {
                    { "sub",  usuario.Email},
                };

            var chaveAssinatura = "77eca19f0bd67c0f788070c1f9ed327b";
            var token = JWT.Encode(payload, Encoding.UTF8.GetBytes(chaveAssinatura), JwsAlgorithm.HS256);
            var retorno = new RetornoLoginViewModel
            {
                Token = token,
            };
            return retorno;
        }
    }
}
