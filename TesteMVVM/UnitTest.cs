using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WPFMVVMCRUD.Model;
using WPFMVVMCRUD.Service;
using WPFMVVMCRUD.ViewModel;
using Moq;

namespace TesteMVVM
{
    [TestClass]
    public class UnitTest
    {
        private Acoes _acao;
        private Fundos _fundos;
        private IBanco _service;

        [TestInitialize]
        public void IniciarTestes()
        {
            _service = new SqlServe();
            _acao = new Acoes();
            _fundos = new Fundos();
        }

        [TestMethod]
        public void CriarAcao_AcaocriadaeretornadaTeste()
        {     
            _acao.NomeAtivo = "TesteAcao";
            _acao.CodigoAtivo = "acao";
            _acao.Valor = 15.0f;
            _acao.DataCotacao = new DateTime(2008, 5, 1, 8, 30, 52);
            _acao.Tipo = Tipo.Acoes;
            _acao.ObsAcoes = "Teste de ações";
            Acoes novaAcao = _service.CriarAcao(_acao);
            Assert.IsNotNull(novaAcao);
            _service.Deletar(novaAcao.Id, "acoes");
        }
        [TestMethod]
        public void testCriarFundo()
        {
            _fundos.NomeAtivo = "TesteFundo";
            _fundos.CodigoAtivo = "fundo";
            _fundos.Valor = 15.0f;
            _fundos.DataCotacao = new DateTime(2008, 5, 1, 8, 30, 52);
            _fundos.Tipo = Tipo.Fundos;
            _fundos.ValorDividendos = 20.0f;
            _fundos.ObsFundo = "Teste de Fundo";
            Fundos novoFundo = _service.CriarFundo(_fundos);
            Assert.IsNotNull(novoFundo);
            _service.Deletar(novoFundo.Id, "fundos");
        }
        [TestMethod]
        public void testListarFundosAcoes()
        {
            _acao.NomeAtivo = "TesteAcao";
            _acao.CodigoAtivo = "acao";
            _acao.Valor = 15.0f;
            _acao.DataCotacao = new DateTime(2008, 5, 1, 8, 30, 52);
            _acao.Tipo = Tipo.Acoes;
            _acao.ObsAcoes = "Teste de ações";
            Acoes novaAcao = _service.CriarAcao(_acao);
            Assert.IsNotNull(novaAcao);
            List<IProdutosFinanceiros> lista = _service.ListarAtivos();
            Assert.IsTrue(lista.Count > 0);
            _service.Deletar(novaAcao.Id, "acoes");

        }
        [TestMethod]
        public void testDeletarAcoes()
        {
            _acao.NomeAtivo = "TesteAcao";
            _acao.CodigoAtivo = "acao";
            _acao.Valor = 15.0f;
            _acao.DataCotacao = new DateTime(2008, 5, 1, 8, 30, 52);
            _acao.Tipo = Tipo.Acoes;
            _acao.ObsAcoes = "Teste de ações";
            Acoes novaAcao = _service.CriarAcao(_acao);
            bool retorno = _service.Deletar(novaAcao.Id, "acoes");
            Assert.IsTrue(retorno);
        }
        [TestMethod]
        public void testDeletarFundos()
        {
            _fundos.NomeAtivo = "TesteFundo";
            _fundos.CodigoAtivo = "fundo";
            _fundos.Valor = 15.0f;
            _fundos.DataCotacao = new DateTime(2008, 5, 1, 8, 30, 52);
            _fundos.Tipo = Tipo.Fundos;
            _fundos.ValorDividendos = 20.0f;
            _fundos.ObsFundo = "Teste de Fundo";
            Fundos novoFundo = _service.CriarFundo(_fundos);
            bool retorno = _service.Deletar(novoFundo.Id, "fundos");
            Assert.IsTrue(retorno);
        }
        [TestCleanup]
        public void Finalteste()
        {

        }
    }
}
