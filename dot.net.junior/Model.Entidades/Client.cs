using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entidades
{
    public class Client
    {
        public int id { get; set; }
        public string name { get; set; }

        public string type_client { get; set; }

        public string cpf_cnpj { get; set; }

        public int address_id { get; set; }

    }
}
