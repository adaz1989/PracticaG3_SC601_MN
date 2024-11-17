using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace G3_Practice.Models
{
    public class EstadoPedido
    {
        [Key]
        public int EstadoId { get; set; }
        public string Descripcion { get; set; }

        // Relación con Pedidos
        public virtual ICollection<Pedidos> Pedidos { get; set; }
    }
}