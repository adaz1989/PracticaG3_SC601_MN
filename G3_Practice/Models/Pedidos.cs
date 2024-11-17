using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace G3_Practice.Models
{
    public class Pedidos
    {
        [Key]
        public int PedidoId { get; set; }
        public string UsuarioId { get; set; }
        public DateTime FechaPedido  { get; set; }
        public int EstadoId { get; set; }
        public String DireccionEntrega { get; set; }
        public String MetodoPago { get; set; }
        public float total { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public ICollection<Ventas> Venta { get; set; }
        public virtual ICollection<PedidoDetalles> PedidoDetalle { get; set; }

        [ForeignKey("EstadoId")]
        public virtual EstadoPedido EstadoPedido { get; set; }
}
}