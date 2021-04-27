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
        public ObservableCollection<IProdutosFinanceiros> ProdutosFinanceiro { get; private set; }

        private IProdutosFinanceiros _produtoSelecionado;
        public IProdutosFinanceiros ProdutoSelecionado
        {
            get { return _produtoSelecionado; }
            set
            {
                SetField(ref _produtoSelecionado, value);
            }
        }
        private IComandoBanco bancoComando { get; set; } //tipo IT
        public ProdutosViewModel()
        {
            this.bancoComando = new SqlServe();
            ProdutosFinanceiro = new ObservableCollection<IProdutosFinanceiros>(bancoComando.ListarAtivos());

            this.Comandos();
        }
        public ICommand AddAcoes { get; private set; }
        public ICommand AddFundo { get; private set; }
        public ICommand Delete { get; private set; }
        public ICommand Update { get; private set; }

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
                       Fundos resultado = this.bancoComando.CriarFundo(produtofinanceiro);
                       this.ProdutosFinanceiro.Add(resultado);
                   }
               },
                (object Param) => { return this.ProdutosFinanceiro.Count < 100; }

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
                       Acoes resultado = this.bancoComando.CriarAcao(produtofinanceiro);
                       if (resultado != null)
                       {
                           ProdutosFinanceiro.Add(resultado);
                       }
                       else
                       {

                       }
                   }
               },
                (object Param) => { return this.ProdutosFinanceiro.Count < 100; }

                );

            this.Delete = new RelayCommand(
            (object param) =>
            {
                bool Deletado = (this.bancoComando.Deletar(this.ProdutoSelecionado.Id, this.ProdutoSelecionado.Tipo.ToString())) ? this.ProdutosFinanceiro.Remove(this.ProdutoSelecionado) : false;

            },
             (object Param) => { return this.ProdutoSelecionado != null; }

             );

            this.Update = new RelayCommand(
        (object param) =>
        {
            var cloneProdutoFinanceiro = this.ProdutoSelecionado;

            if (this.ProdutoSelecionado.Tipo == Model.Tipo.Acoes)
            {
                AcoesWindow fw = new AcoesWindow();
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
            else
            {
                var fw = new FundoWindow();
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

