using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace G3_Practice.Models
{
    public class CarritoDetalle
    {
        [Key]
        public int CarritoDetalleId { get; set; }
        public string UsuarioId { get; set; }
        public int ProuctoId {  get; set; }
        public int Cantiad { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Productos Producto { get; set; }
    }
}