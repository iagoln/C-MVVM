using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVMCRUD.Model;
using WPFMVVMCRUD.ViewModel;

namespace WPFMVVMCRUD.Service
{
   public class MonkServe 
    {
        public Acoes CriarAcao1 { get; set; }
        public Fundos CriarFundo1 { get; set; }
        public List<IProdutosFinanceiros> ListarAtivos1 { get; set; }
        public bool Deletar1 { get; set; }
        public Acoes CriarAcao(Acoes produtofinanceiro)
        {
            return produtofinanceiro;
        }
        public Fundos CriarFundo(Fundos produtofinanceiro)
        {
            return produtofinanceiro;
        }
        public List<IProdutosFinanceiros> ListarAtivos()
        {
            return ListarAtivos1;
        }
        public bool Deletar(int Id, String NomeTabela)
        {
            return true;
        }
        public bool Editar(IProdutosFinanceiros produtosFinanceiros)
        {
            return true;
        }
    }
}
