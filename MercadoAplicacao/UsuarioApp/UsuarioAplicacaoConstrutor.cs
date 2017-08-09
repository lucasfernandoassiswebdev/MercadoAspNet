using Mercado.RepositorioADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.Aplicacao.UsuarioApp
{
    public class UsuarioAplicacaoConstrutor
    {
        public static UsuarioAplicacao UsuarioAplicacaoADO()
        {
            return new UsuarioAplicacao(new UsuarioRepositorioADO());
        }
    }
}
