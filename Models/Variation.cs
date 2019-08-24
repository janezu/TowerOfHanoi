using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;

namespace TowerOfHanoi.Models
{
    public class Variation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VariationID { get; set; }
        public string Connections { get; set; }
        public string VarPic { get; set; }
        public int TowerN { get; set; }
        public string Code { get; set; }
        public bool Directed { get; set; }

        public virtual ICollection<Configuration> Configurations { get; set; }
    }
}
