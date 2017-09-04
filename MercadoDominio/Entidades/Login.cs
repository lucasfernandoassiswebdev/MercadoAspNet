using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MercadoDominio.Entidades
{
    public class Login
    {
        public  int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório escolher um login")]
        [StringLength(50, ErrorMessage = "O login deverá ter no mínimo 5 letras e no máximo 50", MinimumLength = 5)]
        [DisplayName("Login desejado: ")]
        public string LoginU { get; set; }

        [Required(ErrorMessage = "É obrigatório selecionar um usuário")]
        [DisplayName("Usuário: ")]
        public int Usuario { get; set; }


        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(25, ErrorMessage = "A senha deve ter no mínimo 3 caracteres e no maximo 25", MinimumLength = 3)]
        [DisplayName("Senha: ")]
        public string Senha { get; set; }

        public Usuario Funcionario { get; set; }
    }
}
