using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace G3_Practice.Models
{
    public class Ventas
    {
        [Key]
        public int VentaId { get; set; }
        public int PedidoId { get; set; }
        public DateTime FechaVenta { get; set; }
        public string MetodoPago {  get; set; }
        public float TotalVenta { get; set; }
        public virtual Pedidos Pedido { get; set; }

    }
}