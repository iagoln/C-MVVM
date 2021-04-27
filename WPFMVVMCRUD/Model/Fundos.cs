using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVMCRUD.ViewModel;

namespace WPFMVVMCRUD.Model
{
    class Fundos : BaseNotifyPropertyChanged, ICloneable, IProdutosFinanceiros
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetField(ref _id, value); }
        }

        private string _nomeAtivo;
        public string NomeAtivo
        {
            get { return _nomeAtivo; }
            set { SetField(ref _nomeAtivo, value); }
        }

        private string _codigoAtivo;
        public string CodigoAtivo
        {
            get { return _codigoAtivo; }
            set { SetField(ref _codigoAtivo, value); }
        }

        private float _valor;
        public float Valor
        {
            get { return _valor; }
            set { SetField(ref _valor, value); }
        }

        private DateTime _dataCotacao;
        public DateTime DataCotacao
        {
            get { return _dataCotacao; }
            set { SetField(ref _dataCotacao, value); }
        }

        private Tipo _tipo;
        public Tipo Tipo
        {
            get { return _tipo; }
            set { SetField(ref _tipo, value); }
        }
        private float _valorDividendos;
        public float ValorDividendos
        {
            get { return _valorDividendos; }
            set { SetField(ref _valorDividendos, value); }
        }
        private string _obsFundo;
        public string ObsFundo
        {
            get { return _obsFundo; }
            set { SetField(ref _obsFundo, value); }
        }
    
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
