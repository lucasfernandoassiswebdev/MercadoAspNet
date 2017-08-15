using Mercado.Dominio;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MercadoDominio.Entidades
{
    public class Login
    {
        [Required(ErrorMessage = "É obrigatório escolher um login")]
        [DisplayName("Login desejado: ")]
        public string login { get; set; }

        [Required(ErrorMessage = "É obrigatório selecionar um usuário")]
        [DisplayName("Usuário: ")]
        public int usuario { get; set; }


        [Required(ErrorMessage = "A senha é obrigatório")]
        [DisplayName("Senha: ")]
        public string senha { get; set; }

        public Usuario funcionario;
    }
}
