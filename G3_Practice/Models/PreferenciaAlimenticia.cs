using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace G3_Practice.Models
{
    public class PreferenciaAlimenticia
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}