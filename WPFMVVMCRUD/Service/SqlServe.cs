using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFMVVMCRUD.Model;
using WPFMVVMCRUD.ViewModel;

namespace WPFMVVMCRUD.Service
{
    public class SqlServe : IBanco
    {
        public Conexao conexao { get; private set; }
        public SqlCommand cmd { get; private set; }

        public SqlServe()
        {
            this.conexao = new Conexao(new SqlConnection(@"Data Source=DESKTOP-JOH5270\SQLEXPRESS;Initial Catalog=luztreinamento;Integrated Security=True"));
            this.cmd = new SqlCommand();
            this.cmd.Connection = (SqlConnection)conexao.conectar();
        }
        public IProdutosFinanceiros CriarAtivo(IProdutosFinanceiros produtosFinanceiros)
        {
            if (produtosFinanceiros.Tipo == Tipo.Acoes)
            {
                return CriarAcao((Acoes)produtosFinanceiros);
            }
            if (produtosFinanceiros.Tipo == Tipo.Fundos)
            {
                return CriarFundo((Fundos)produtosFinanceiros);
            }
            return null;
        }

        public bool Deletar(int Id, String NomeTabela)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "DELETE from " + NomeTabela + " where Id=" + Id;
                conexao.conectar();
                cmd.ExecuteNonQuery();
                conexao.desconectar();
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro em conexão do banco de dados: " + e.Message);
                return false;
            }
            return true;
        }
        public bool Editar(IProdutosFinanceiros produtosFinanceiros)
        {
            try
            {
                if (Tipo.Fundos == produtosFinanceiros.Tipo)
                {
                    return EditarFundo(produtosFinanceiros);
                }
                else
                {
                    return EditarAcao(produtosFinanceiros);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro em conexão do banco de dados: " + e.Message);
                return false;
            }
           
        }
        public List<IProdutosFinanceiros> ListarAtivos()
        {
            return ListarFundos().Concat(ListarAcoes()).ToList();
        }
        public List<IProdutosFinanceiros> ListarAcoes()
        {
            List<IProdutosFinanceiros> _iprodutosFinanceiros = new List<IProdutosFinanceiros>();
            try
            {
                cmd.Parameters.Clear();
                SqlDataReader reader = null;
                cmd.CommandText = "select * from acoes";
                cmd.Connection = (SqlConnection)conexao.conectar();
                conexao.conectar();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _iprodutosFinanceiros.Add(new Acoes()
                    {
                        Id = (int)reader["Id"],
                        NomeAtivo = (string)reader["NomeAtivo"],
                        CodigoAtivo = (string)reader["CodigoAtivo"],
                        Valor = float.Parse(reader["Valor"].ToString()),
                        DataCotacao = (DateTime)reader["DataCotacao"],
                        Tipo = Model.Tipo.Acoes,
                        ObsAcoes = (string)reader["ObsAcoes"]
                    });
                }
                return _iprodutosFinanceiros;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro em conexão do banco de dados: " + e.Message);
                return _iprodutosFinanceiros;
            }
            finally
            {
                conexao.desconectar();
            }
        }
        public List<IProdutosFinanceiros> ListarFundos()
        {
            List<IProdutosFinanceiros> _iprodutosFinanceiros = new List<IProdutosFinanceiros>();
            try
            {
                SqlDataReader reader = null;
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from fundos";
                conexao.conectar();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _iprodutosFinanceiros.Add(new Fundos()
                    {
                        Id = (int)reader["Id"],
                        NomeAtivo = (string)reader["NomeAtivo"],
                        CodigoAtivo = (string)reader["CodigoAtivo"],
                        Valor = float.Parse(reader["Valor"].ToString()),
                        DataCotacao = (DateTime)reader["DataCotacao"],
                        Tipo = Model.Tipo.Fundos,
                        ObsFundo = (string)reader["ObsFundo"],
                        ValorDividendos = float.Parse(reader["ValorDividendos"].ToString())
                    });
                }
                conexao.desconectar();
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro em conexão do banco de dados: " + e.Message);
            }
            finally
            {
                conexao.desconectar();
            }
            return _iprodutosFinanceiros;
        }
        public Acoes CriarAcao(Acoes produtofinanceiro)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into acoes (NomeAtivo,CodigoAtivo,Valor,DataCotacao,Tipo,ObsAcoes) output INSERTED.ID  values  (@NomeAtivo,@CodigoAtivo,@Valor,@DataCotacao,@Tipo,@ObsAcoes); ";
                cmd.Parameters.AddWithValue("@NomeAtivo", produtofinanceiro.NomeAtivo);
                cmd.Parameters.AddWithValue("@CodigoAtivo", produtofinanceiro.CodigoAtivo);
                cmd.Parameters.AddWithValue("@Valor", produtofinanceiro.Valor);
                cmd.Parameters.AddWithValue("@DataCotacao", produtofinanceiro.DataCotacao);
                cmd.Parameters.AddWithValue("@Tipo", produtofinanceiro.Tipo);
                cmd.Parameters.AddWithValue("@ObsAcoes", produtofinanceiro.ObsAcoes);
                conexao.conectar();
                int idAtivo = (int)cmd.ExecuteScalar();
                produtofinanceiro.Id = idAtivo;
                conexao.desconectar();
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro em conexão do banco de dados: " + e.Message);
                return produtofinanceiro = null;
            }
            return produtofinanceiro;
        }
        public Fundos CriarFundo(Fundos produtofinanceiro)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into fundos (NomeAtivo,CodigoAtivo,Valor,DataCotacao,Tipo,ValorDividendos,ObsFundo) output INSERTED.ID  values  (@NomeAtivo,@CodigoAtivo,@Valor,@DataCotacao,@Tipo,@ValorDividendos,@ObsFundo); ";
                cmd.Parameters.AddWithValue("@Id", produtofinanceiro.Id);
                cmd.Parameters.AddWithValue("@NomeAtivo", produtofinanceiro.NomeAtivo);
                cmd.Parameters.AddWithValue("@CodigoAtivo", produtofinanceiro.CodigoAtivo);
                cmd.Parameters.AddWithValue("@Valor", produtofinanceiro.Valor);
                cmd.Parameters.AddWithValue("@DataCotacao", produtofinanceiro.DataCotacao);
                cmd.Parameters.AddWithValue("@Tipo", produtofinanceiro.Tipo);
                cmd.Parameters.AddWithValue("@ValorDividendos", produtofinanceiro.ValorDividendos);
                cmd.Parameters.AddWithValue("@ObsFundo", produtofinanceiro.ObsFundo);
                conexao.conectar();
                int idAtivo = (int)cmd.ExecuteScalar();
                produtofinanceiro.Id = idAtivo;
                conexao.desconectar();
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro em conexão do banco de dados: " + e.Message);
                return produtofinanceiro = null;
            }
            return produtofinanceiro;
        }
        private bool EditarFundo(IProdutosFinanceiros produtosFinanceiros)
        {
            try
            {
                Fundos produto = (Fundos)produtosFinanceiros;
                cmd.Parameters.Clear();
                cmd.CommandText = "UPDATE Fundos SET NomeAtivo = @NomeAtivo, CodigoAtivo = @CodigoAtivo, Valor = @Valor, DataCotacao = @DataCotacao, Tipo = @Tipo , ValorDividendos = @ValorDividendos, ObsFundo = @ObsFundo WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", produto.Id);
                cmd.Parameters.AddWithValue("@NomeAtivo", produto.NomeAtivo);
                cmd.Parameters.AddWithValue("@CodigoAtivo", produto.CodigoAtivo);
                cmd.Parameters.AddWithValue("@Valor", produto.Valor);
                cmd.Parameters.AddWithValue("@DataCotacao", produto.DataCotacao);
                cmd.Parameters.AddWithValue("@Tipo", produto.Tipo);
                cmd.Parameters.AddWithValue("@ValorDividendos", produto.ValorDividendos);
                cmd.Parameters.AddWithValue("@ObsFundo", produto.ObsFundo);
                conexao.conectar();
                cmd.ExecuteNonQuery();
                conexao.desconectar();
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro em conexão do banco de dados: " + e.Message);
                return false;
            }
            return true;
        }
        private bool EditarAcao(IProdutosFinanceiros produtosFinanceiros)
        {
            try
            {
                Acoes produto = (Acoes)produtosFinanceiros;
            cmd.Parameters.Clear();
            cmd.CommandText = "UPDATE Acoes SET NomeAtivo = @NomeAtivo, CodigoAtivo = @CodigoAtivo, Valor = @Valor, DataCotacao = @DataCotacao, Tipo = @Tipo , ObsAcoes = @ObsAcoes WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", produto.Id);
            cmd.Parameters.AddWithValue("@NomeAtivo", produto.NomeAtivo);
            cmd.Parameters.AddWithValue("@CodigoAtivo", produto.CodigoAtivo);
            cmd.Parameters.AddWithValue("@Valor", produto.Valor);
            cmd.Parameters.AddWithValue("@DataCotacao", produto.DataCotacao);
            cmd.Parameters.AddWithValue("@Tipo", produto.Tipo);
            cmd.Parameters.AddWithValue("@ObsAcoes", produto.ObsAcoes);
            conexao.conectar();
            cmd.ExecuteNonQuery();
            conexao.desconectar();
            return true;
        }
            catch (Exception e)
            {
                MessageBox.Show("Erro em conexão do banco de dados: " + e.Message);
                return false;
            }
            
        }
    }
}
