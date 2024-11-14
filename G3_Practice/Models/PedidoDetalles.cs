using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace G3_Practice.Models
{
    public class PedidoDetalles
    {
        [Key]
        public int PedidoDetalleId { get; set; } 
        public int PedidoId {  get; set; }
        public int ProductoId {  get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public float SubTotal {  get; set; }

        public virtual Pedidos Pedido { get; set; }
        public virtual Productos Producto { get; set; }
    }
}