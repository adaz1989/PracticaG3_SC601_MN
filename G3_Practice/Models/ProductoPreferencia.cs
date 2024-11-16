using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G3_Practice.Models
{
    public class ProductoPreferencia
    {
        public int ProductoId { get; set; }
        public virtual Productos Productos { get; set; }

        public int PreferenciaAlimenticiaId { get; set; }
        public virtual PreferenciaAlimenticia PreferenciaAlimenticia { get; set; }
    }


}