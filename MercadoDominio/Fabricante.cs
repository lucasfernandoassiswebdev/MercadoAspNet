using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mercado.Dominio
{
    public class Fabricante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo do nome do fabricante")]
        [DisplayName("Nome do fabricante: ")]
        public string Nome { get; set; }

        public IEnumerable<Produto> Produtos { get; set; }
    }
}
