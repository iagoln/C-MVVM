using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVMCRUD.Model;
using WPFMVVMCRUD.Service;
using WPFMVVMCRUD.ViewModel;
using Moq;

namespace TesteMVVM
{
    [TestClass]
    class testeMoq
    {
        private Acoes _acao;
        private Fundos _fundos;
        private IBanco _service;
   

        [TestInitialize]
        public void IniciarTestes()
        {
            //_service = new MonkServe();
            //_acao = new Acoes();
            //_fundos = new Fundos();
       
        }
      

    }
}
