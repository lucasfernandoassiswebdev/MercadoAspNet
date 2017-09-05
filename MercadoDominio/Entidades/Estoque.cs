using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MercadoDominio.Entidades
{
    public class Estoque
    {  
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecione 1 produto")]
        [DisplayName("Produto: ")]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "Preencha a quantidade")]
        [DisplayName("Quantidade: ")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Valor inválido, digite apenas números e lembrese de colocar no máximo 2 casas após a vírgula.")]
        [Range(1, 10000, ErrorMessage = "Valor informado inválido")]
        public decimal Quantidade { get; set; }

        public Produto Produto { get; set; }
    }
}
