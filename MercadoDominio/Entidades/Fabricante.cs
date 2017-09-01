using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MercadoDominio.Entidades
{
    public class Fabricante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo do nome do fabricante")]
        [DisplayName("Nome do fabricante: ")]
        [StringLength(50, ErrorMessage = "O nome do fabricante deve ter no mínimo 2 letras e no máximo 50", MinimumLength = 2)]
        public string Nome { get; set; }
    }
}
