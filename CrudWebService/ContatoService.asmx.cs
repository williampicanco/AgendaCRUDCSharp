using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Services;
using CrudWebService.Models;

namespace CrudWebService
{
    /// <summary>
    /// Summary description for ContatoService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ContatoService : System.Web.Services.WebService
    {

      
        [WebMethod]
        public int Inserir(Contato obj)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)               
                    db.Open();               
                DynamicParameters p = new DynamicParameters();
                p.Add("@ContatoID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.AddDynamicParams(new { Nome = obj.Nome, TelefoneFixo = obj.TelefoneFixo, TelefoneCelular = obj.TelefoneCelular, CPF = obj.CPF, Email = obj.Email, ImagemUrl = obj.ImagemUrl });
                int result = db.Execute("sp_Contatos_Inserir", p, commandType: CommandType.StoredProcedure);
                if (result != 0)             
                    return p.Get<int>("ContatoID");               
                return 0;
            }
        }

        [WebMethod]
        public bool Update(Contato obj)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)             
                    db.Open();             
                 int result = db.Execute("sp_Contatos_Atualizar", new { obj.ContatoID, Nome = obj.Nome, TelefoneFixo = obj.TelefoneFixo, TelefoneCelular = obj.TelefoneCelular, CPF = obj.CPF, Email = obj.Email, ImagemUrl = obj.ImagemUrl }, commandType: CommandType.StoredProcedure);
                return result != 0;
            }

        }

        [WebMethod]
        public List<Contato> GetAll()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)              
                    db.Open();             
                return db.Query<Contato>("Select * from Contatos", commandType: CommandType.Text).ToList();
            }
        }

        [WebMethod]
        public bool Delete(int contaID)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                int resultado = db.Execute("delete from Contatos where ContatoID= @ContatoID", new { ContatoID = contaID }, commandType: CommandType.Text);
                return resultado != 0;
            }
        }
    }
}
