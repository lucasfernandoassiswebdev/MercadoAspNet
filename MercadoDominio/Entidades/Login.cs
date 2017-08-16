using MercadoDominio.Entidades;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MercadoDominio.Entidades
{
    public class Login
    {
        public  int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório escolher um login")]
        [DisplayName("Login desejado: ")]
        public string LoginU { get; set; }

        [Required(ErrorMessage = "É obrigatório selecionar um usuário")]
        [DisplayName("Usuário: ")]
        public int Usuario { get; set; }


        [Required(ErrorMessage = "A senha é obrigatória")]
        [DisplayName("Senha: ")]
        public string Senha { get; set; }

        public Usuario Funcionario { get; set; }
    }
}
