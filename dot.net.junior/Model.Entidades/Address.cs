using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entidades
{
    public class Address
    {
        public int id { get; set; }
        public int number { get; set; }

        public string road { get; set; }

        public string district { get; set; }
        public string city { get; set; }

        public string cep { get; set; }
        public string type_address { get; set; }

        public string complement { get; set; }
    }
}
