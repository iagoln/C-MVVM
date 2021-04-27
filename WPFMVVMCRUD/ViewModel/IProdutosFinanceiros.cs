using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMVVMCRUD.ViewModel
{
    public interface IProdutosFinanceiros
    {
      
        int Id
        {
            get;
            set;
        }
        string NomeAtivo
        {
            get;
            set;
        }

        string CodigoAtivo
        {
            get;
            set;
        }
        float Valor
        {
            get;
            set;
        }
        DateTime DataCotacao
        {
            get;
            set;
        }
        Model.Tipo Tipo
        {
            get;
            set;
        }
       

    }


}
