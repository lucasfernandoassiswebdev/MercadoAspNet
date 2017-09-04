using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MercadoDominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo do nome do usuario")]
        [DisplayName("Nome do usuario: ")]
        [StringLength(75, ErrorMessage = "O nome do usuario deve ter no mínimo 2 e no máximo 75 letras", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Selecione o nivel do usuário")]
        [DisplayName("Nível: ")]
        public string Nivel { get; set; }
    }
}
