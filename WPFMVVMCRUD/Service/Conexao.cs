using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMVVMCRUD.Service
{
   public class Conexao
    {
        private IDbConnection con;
        //public SqlConnection con;
        public Conexao(IDbConnection con)
        {
            this.con = con;
            // this.con = new SqlConnection();
            // con.ConnectionString = @"Data Source=DESKTOP-JOH5270\SQLEXPRESS;Initial Catalog=luztreinamento;Integrated Security=True";
        }
    
        public IDbConnection conectar()
        {
            if (con.State == ConnectionState.Closed )
            {
                con.Open();
            }
            return con;
        }
        public void desconectar()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
