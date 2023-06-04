using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoToken
    {
        private string chaveAssinatura = "77eca19f0bd67c0f788070c1f9ed327b";
        public string GerarToken(string sub)
        {
            try
            {
                var payload = new Dictionary<string, object>
                {

                    { "sub",  sub},
                    { "exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds() }
                };
                var token = JWT.Encode(payload, Encoding.UTF8.GetBytes(chaveAssinatura), JwsAlgorithm.HS256);
                return token;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public string ValidarToken(string token)
        {
            try
            {
                var jwt = token.Replace(" ", "");

                // Corrige a codificação Base64Url
                jwt = jwt.Replace('-', '+').Replace('_', '/');

                // Preenche a string JWT com caracteres de preenchimento, se necessário
                switch (jwt.Length % 4)
                {
                    case 2: jwt += "=="; break;
                    case 3: jwt += "="; break;
                }
                var payload = JWT.Decode<Dictionary<string, object>>(jwt, Encoding.UTF8.GetBytes(chaveAssinatura), JwsAlgorithm.HS256);
                payload.TryGetValue("sub", out var sub);
                if (sub != null)
                {
                    var user = (string)sub;
                    var expiracao = DateTimeOffset.FromUnixTimeSeconds((long)payload["exp"]);

                    if (expiracao > DateTimeOffset.UtcNow)
                    {
                        return user;
                    }
                }
            }

            catch (Exception)
            {
                return null;
            }

            return null;
        }
    }
}
