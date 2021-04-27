using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVMCRUD.Model;
using WPFMVVMCRUD.ViewModel;

namespace WPFMVVMCRUD.Service
{
    class SqlServe : IComandoBanco
    {
   

        public virtual Acoes CriarAcao(Acoes produtofinanceiro)
        {
            try
            {
                Conexao conexao = new Conexao();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into acoes (NomeAtivo,CodigoAtivo,Valor,DataCotacao,Tipo,ObsAcoes) output INSERTED.ID  values  (@NomeAtivo,@CodigoAtivo,@Valor,@DataCotacao,@Tipo,@ObsAcoes);  ";
                cmd.Parameters.AddWithValue("@Id", produtofinanceiro.Id);
                cmd.Parameters.AddWithValue("@NomeAtivo", produtofinanceiro.NomeAtivo);
                cmd.Parameters.AddWithValue("@CodigoAtivo", produtofinanceiro.CodigoAtivo);
                cmd.Parameters.AddWithValue("@Valor", produtofinanceiro.Valor);
                cmd.Parameters.AddWithValue("@DataCotacao", produtofinanceiro.DataCotacao);
                cmd.Parameters.AddWithValue("@Tipo", produtofinanceiro.Tipo);
                cmd.Parameters.AddWithValue("@ObsAcoes", produtofinanceiro.ObsAcoes);
                cmd.Connection = conexao.conectar();
                int idAtivo = (int)cmd.ExecuteScalar();
                produtofinanceiro.Id = idAtivo;
                conexao.desconectar();
            }
            catch (Exception ex)
            {
                return produtofinanceiro = null;
            }
            return produtofinanceiro;
        }
        public virtual Fundos CriarFundo(Fundos produtofinanceiro)
        {
            Conexao conexao = new Conexao();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into fundos (NomeAtivo,CodigoAtivo,Valor,DataCotacao,Tipo,ValorDividendos,ObsFundo) output INSERTED.ID  values  (@NomeAtivo,@CodigoAtivo,@Valor,@DataCotacao,@Tipo,@ValorDividendos,@ObsFundo); ";
            cmd.Parameters.AddWithValue("@Id", produtofinanceiro.Id);
            cmd.Parameters.AddWithValue("@NomeAtivo", produtofinanceiro.NomeAtivo);
            cmd.Parameters.AddWithValue("@CodigoAtivo", produtofinanceiro.CodigoAtivo);
            cmd.Parameters.AddWithValue("@Valor", produtofinanceiro.Valor);
            cmd.Parameters.AddWithValue("@DataCotacao", produtofinanceiro.DataCotacao);
            cmd.Parameters.AddWithValue("@Tipo", produtofinanceiro.Tipo);
            cmd.Parameters.AddWithValue("@ValorDividendos", produtofinanceiro.ValorDividendos);
            cmd.Parameters.AddWithValue("@ObsFundo", produtofinanceiro.ObsFundo);
            cmd.Connection = conexao.conectar();
            int idAtivo = (int)cmd.ExecuteScalar();
            produtofinanceiro.Id = idAtivo;
            conexao.desconectar();

            return produtofinanceiro;
        }
        public bool Deletar( int Id , String NomeTabela)
        {
            try
            {
                Conexao conexao = new Conexao();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE from " + NomeTabela + " where Id=" + Id;
                cmd.Connection = conexao.conectar();
                cmd.ExecuteNonQuery();
                conexao.desconectar();
            }
            catch (Exception e)
            {
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
                Fundos produto = (Fundos)produtosFinanceiros;
                Conexao conexao = new Conexao();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE Fundos SET NomeAtivo = @NomeAtivo, CodigoAtivo = @CodigoAtivo, Valor = @Valor, DataCotacao = @DataCotacao, Tipo = @Tipo , ValorDividendos = @ValorDividendos, ObsFundo = @ObsFundo WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", produto.Id);
                cmd.Parameters.AddWithValue("@NomeAtivo", produto.NomeAtivo);
                cmd.Parameters.AddWithValue("@CodigoAtivo", produto.CodigoAtivo);
                cmd.Parameters.AddWithValue("@Valor", produto.Valor);
                cmd.Parameters.AddWithValue("@DataCotacao", produto.DataCotacao);
                cmd.Parameters.AddWithValue("@Tipo", produto.Tipo);
                cmd.Parameters.AddWithValue("@ValorDividendos", produto.ValorDividendos);
                cmd.Parameters.AddWithValue("@ObsFundo", produto.ObsFundo);
                cmd.Connection = conexao.conectar();
                cmd.ExecuteNonQuery();
                conexao.desconectar();
            } else
            {
                Acoes produto = (Acoes)produtosFinanceiros;
                Conexao conexao = new Conexao();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE Acoes SET NomeAtivo = @NomeAtivo, CodigoAtivo = @CodigoAtivo, Valor = @Valor, DataCotacao = @DataCotacao, Tipo = @Tipo , ObsAcoes = @ObsAcoes WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", produto.Id);
                cmd.Parameters.AddWithValue("@NomeAtivo", produto.NomeAtivo);
                cmd.Parameters.AddWithValue("@CodigoAtivo", produto.CodigoAtivo);
                cmd.Parameters.AddWithValue("@Valor", produto.Valor);
                cmd.Parameters.AddWithValue("@DataCotacao", produto.DataCotacao);
                cmd.Parameters.AddWithValue("@Tipo", produto.Tipo);
                cmd.Parameters.AddWithValue("@ObsAcoes", produto.ObsAcoes);
                cmd.Connection = conexao.conectar();
                cmd.ExecuteNonQuery();
                conexao.desconectar();
            }           
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public List<IProdutosFinanceiros> ListarAtivos( )
        {

            List<IProdutosFinanceiros> _iprodutosFinanceiros = new List<IProdutosFinanceiros>();
            Conexao conexao = new Conexao();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "select * from acoes";
            cmd.Connection = conexao.conectar();
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
             conexao.desconectar();

            cmd.CommandText = "select * from fundos";
            cmd.Connection = conexao.conectar();
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
            return _iprodutosFinanceiros;
        }


    }
}
