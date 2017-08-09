using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mercado.Dominio
{
    public class Venda
    {
        public int IdVenda { get; set; }

        [Required(ErrorMessage = "Selecione um produto")]
        [DisplayName("Produto: ")]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "Digite a quantidade")]
        [DisplayName("Quantidade: ")]
        public decimal Quantidade { get; set; }

        [Required(ErrorMessage = "Selecione o funcionário")]
        [DisplayName("Funcionário: ")]
        public Int32 Funcionario { get; set; }
    }
}
