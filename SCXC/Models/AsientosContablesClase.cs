using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCXC.Models
{
    public class AsientosContablesClase
    {
        public string Description { get; set; }
        public int IdAuxiliarSystem { get; set; }
        public string MovementType { get; set; }
        public DateTime EntryDate { get; set; }
        public bool Status { get; set; }
    }
}