using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace G3_Practice.Models
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }
        public string ProductoName { get; set; }
        public float ? Price { get; set; }
        public int stock { get; set; }
        public string imagen { get; set; }
        public ICollection<CategoriaxProducto> CategoriaxProductos { get; set; }
    }
}