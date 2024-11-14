using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G3_Practice.Models
{
    public class CategoriaxProducto
    {
        public int ProductoId { get; set; }
        public int CategoriaId { get; set; }
        public virtual Productos Producto { get; set; } // Relación con ApplicationUser
        public virtual Categorias Categoria { get; set; } // Relación con PreferenciaAlimenticia
    }
}