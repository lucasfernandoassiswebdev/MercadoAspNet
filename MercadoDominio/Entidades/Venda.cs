using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MercadoDominio.Entidades
{
    public class Venda
    {
        public int IdVenda { get; set; }

        [Required(ErrorMessage = "Selecione um produto")]
        [DisplayName("Produto: ")]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "Digite a quantidade")]
        [DisplayName("Quantidade: ")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Quantidade inválida, digite apenas 2 casas depois da vírgula")]
        [Range(1, 9999, ErrorMessage = "Valor informado inválido")]
        public decimal Quantidade { get; set; }

        [Required(ErrorMessage = "Selecione o funcionário")]
        [DisplayName("Funcionário: ")]
        public int IdFuncionario { get; set; }

        public string Data { get; set; }

        public Produto Produto { get; set; }
        public Usuario Funcionario { get; set; } 
    }
}
