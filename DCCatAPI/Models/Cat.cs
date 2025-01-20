using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCCatAPI.Models
{
    public class Cat
    {
        public string Id { get; set; } = string.Empty; // Inicialización por defecto
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
    }
}
