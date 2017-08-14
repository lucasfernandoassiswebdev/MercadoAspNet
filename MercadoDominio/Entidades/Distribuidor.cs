using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mercado.Dominio
{
    public class Distribuidor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo do nome do distribuidor")]
        [DisplayName("Nome do distribuidor: ")]
        public string Nome { get; set; }
    }
}
