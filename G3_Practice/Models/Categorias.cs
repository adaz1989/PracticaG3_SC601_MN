using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace G3_Practice.Models
{
    public class Categorias
    {
        [Key]
        public int CategoriaId { get; set; }
        public string TipoCategoria { get; set; }
        public ICollection<CategoriaxProducto> CategoriaxProductos { get; set; }
    }
}