using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Input;
using WPFMVVMCRUD.Model;
using WPFMVVMCRUD.Service;

namespace WPFMVVMCRUD.ViewModel
{
    public class ProdutosViewModel : BaseNotifyPropertyChanged
    {
      
        private IProdutosFinanceiros _produtoSelecionado;
        private IBanco bancoComando;
        public ProdutosViewModel()
        {
            this.bancoComando = new SqlServe();
            ProdutosFinanceiro = new ObservableCollection<IProdutosFinanceiros>(bancoComando.ListarAtivos());
           // ProdutosFinanceiro =  bancoComando.ListarAtivos();
            this.Comandos();
        }
        //public ProdutosViewModel(IBanco conec)
        //{
        //    this.bancoComando = conec;
        //    ProdutosFinanceiro = new ObservableCollection<IProdutosFinanceiros>(this.bancoComando.ListarAtivos());
        //    this.Comandos();
        //}

        public IProdutosFinanceiros ProdutoSelecionado
        {
            get { return _produtoSelecionado; }
            set
            {
                SetField(ref _produtoSelecionado, value);
            }
        }
        public ObservableCollection<IProdutosFinanceiros> ProdutosFinanceiro { get; private set; }

        public ICommand AddAcoes { get; private set; }
        public ICommand AddFundo { get; private set; }
        public ICommand Delete { get; private set; }
        public ICommand Update { get; private set; }
       //public void notificartela()
       // {

       //   this.ProdutosFinanceiro = new List<IProdutosFinanceiros>(this.ProdutosFinanceiro);
       //     RaisePropertyChanged("ProdutosFinanceiro");
       // }
        public void Comandos()
        {

            this.AddFundo = new RelayCommand(
               (object param) =>
               {
                   Fundos produtofinanceiro = new Fundos();
                   produtofinanceiro.Tipo = Model.Tipo.Fundos;
                   FundoWindow fw = new FundoWindow();
                   fw.DataContext = produtofinanceiro;
                   fw.ShowDialog();
                   if (fw.DialogResult.HasValue && fw.DialogResult.Value)
                   {
                       IProdutosFinanceiros resultado = this.bancoComando.CriarAtivo(produtofinanceiro);
                       if (resultado != null)
                       {
                           this.ProdutosFinanceiro.Add(resultado);
                          
                       }
                   }
               }
                );
            this.AddAcoes = new RelayCommand(
               (object param) =>
               {
                   Acoes produtofinanceiro = new Acoes();
                   produtofinanceiro.Tipo = Model.Tipo.Acoes;
                   AcoesWindow aw = new AcoesWindow();
                   aw.DataContext = produtofinanceiro;
                   aw.ShowDialog();
                   if (aw.DialogResult.HasValue && aw.DialogResult.Value)
                   {
                       IProdutosFinanceiros resultado = this.bancoComando.CriarAtivo(produtofinanceiro);
                       if (resultado != null)
                       {
                           ProdutosFinanceiro.Add(resultado);
                       }
                   }
               }
                );

            this.Delete = new RelayCommand(
            (object param) =>
            {
                if (this.bancoComando.Deletar(this.ProdutoSelecionado.Id, this.ProdutoSelecionado.Tipo.ToString()))
                {
                    this.ProdutosFinanceiro.Remove(this.ProdutoSelecionado);
                }
            },
             (object Param) => { return this.ProdutoSelecionado != null; });

            this.Update = new RelayCommand(
        (object param) =>
        {
            IProdutosFinanceiros cloneProdutoFinanceiro = this.ProdutoSelecionado;

            if (this.ProdutoSelecionado.Tipo == Model.Tipo.Acoes)
            {
                AcoesWindow aw = new AcoesWindow();
                aw.DataContext = cloneProdutoFinanceiro;
                aw.ShowDialog();
                if (aw.DialogResult.HasValue && aw.DialogResult.Value)
                {
                    if (this.bancoComando.Editar(cloneProdutoFinanceiro))
                    {
                        this.ProdutoSelecionado = cloneProdutoFinanceiro;
                    }
                }
            }
            else
            {
                FundoWindow fw = new FundoWindow();
                fw.DataContext = cloneProdutoFinanceiro;
                fw.ShowDialog();
                if (fw.DialogResult.HasValue && fw.DialogResult.Value)
                {
                    if (this.bancoComando.Editar(cloneProdutoFinanceiro))
                    {
                        this.ProdutoSelecionado = cloneProdutoFinanceiro;
                    }
                }
            }
        },
         (object Param) => { return this.ProdutoSelecionado != null; }

         );
        }
    }
}

