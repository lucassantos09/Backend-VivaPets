using VivaPetsBackEnd.ObjetoValor;

namespace Dominio.Entidades
{
    public class Animais
    {
        public int Id { get; set; }
        public string Nome{ get; set; }
        public string Raca{ get; set; }
        public string Cor{ get; set; }
        public Servico Servico { get; set; }
        public string Responsavel { get; set; }
        public string Telefone{ get; set; }
    }
}
