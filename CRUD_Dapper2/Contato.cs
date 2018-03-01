using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Dapper2
{
    public class Contato
    {
        public int ContatoID { get; set; }
        public string Nome { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string ImagemUrl { get; set; }
    }
}