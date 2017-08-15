using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MercadoDominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo do nome do usuario")]
        [DisplayName("Nome do usuario: ")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Selecione o nivel do usuário")]
        [DisplayName("Nível: ")]
        public string Nivel { get; set; }
    }
}
