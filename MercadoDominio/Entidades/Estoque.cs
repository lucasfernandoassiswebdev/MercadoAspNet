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
        public decimal Quantidade { get; set; }

        public Produto Produto { get; set; }
    }
}
