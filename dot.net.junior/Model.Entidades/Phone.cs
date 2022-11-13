using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entidades
{
    public class Phone
    {
        public int id { get; set; }
        public string ddd { get; set; }

        public string number { get; set; }

        public string type_phone { get; set; }
        public int client_id { get; set; }
    }
}
