using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Animal
    {
        public enum TipoAnimal { Bovinos, Equinos };
        public string Tipo { get; set; }
        public string NombreAnimal { get; set; }
    }
}
