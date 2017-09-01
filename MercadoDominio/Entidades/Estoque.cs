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
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Valor digitado inválido, lembre-se de inserir apenas números e no máximo 2 casas decimais")]
        public decimal Quantidade { get; set; }

        public Produto Produto { get; set; }
    }
}
