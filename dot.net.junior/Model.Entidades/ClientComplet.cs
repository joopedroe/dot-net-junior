using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entidades
{
    public class ClientComplet
    {
        public Client client { get; set; }
        public Address address { get; set; }
        public Phone phone { get; set; }
    }
}
