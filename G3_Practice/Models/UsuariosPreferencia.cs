using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G3_Practice.Models
{
    public class UsuariosPreferencia
    {
        // Propiedades para las claves foráneas
        public string ApplicationUserId { get; set; } // Id del usuario
        public int PreferenciaAlimenticiaId { get; set; } // Id de la preferencia alimenticia

        // Propiedades de navegación (relaciones)
        public virtual ApplicationUser ApplicationUser { get; set; } // Relación con ApplicationUser
        public virtual PreferenciaAlimenticia PreferenciaAlimenticia { get; set; } // Relación con PreferenciaAlimenticia
    }
}