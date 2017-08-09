using Mercado.RepositorioADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.Aplicacao.VendasApp
{
    public class VendasAplicacaoConstrutor
    {
        public static VendasAplicacao VendaoAplicacaoADO()
        {
            return new VendasAplicacao(new VendaRepositorioADO());
        }
    }
}
