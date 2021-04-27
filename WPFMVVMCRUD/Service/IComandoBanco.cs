using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVMCRUD.Model;
using WPFMVVMCRUD.ViewModel;

namespace WPFMVVMCRUD.Service
{
    interface IComandoBanco
    {
        Acoes CriarAcao(Acoes produtofinanceiro);
        Fundos CriarFundo(Fundos produtofinanceiro);
        List<IProdutosFinanceiros> ListarAtivos();
        bool Deletar(int Id, String NomeTabela);
        bool Editar(IProdutosFinanceiros produtosFinanceiros);
    }
}
