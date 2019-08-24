using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TowerOfHanoi.Models
{
    public class Configuration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConfigurationID { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public string navodilo {get; set; }
        public string conPic { get; set; }
        public int VariationID { get; set; }

        public virtual ICollection<Optimal> Optimals { get; set; }
        public virtual Variation Variation { get; set; }
    }

    public class OptimalCreation
    {
        public int o1 { get; set; }
        public int o2 { get; set; }
        public int o3 { get; set; }
        public int o4 { get; set; }
        public int o5 { get; set; }
        public int o6 { get; set; }
        public int o7 { get; set; }
        public int o8 { get; set; }

    }


    public class ConfigurationCreation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConfigurationID { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public string navodilo { get; set; }
        public string conPic { get; set; }
        public int VariationID { get; set; }
        public int DiskNr { get; set; }
        public int MovesO { get; set; }
        public string Code { get; set; }

        public int o1 { get; set; }
        public int o2 { get; set; }
        public int o3 { get; set; }
        public int o4 { get; set; }
        public int o5 { get; set; }
        public int o6 { get; set; }
        public int o7 { get; set; }
        public int o8 { get; set; }


        public ICollection<Optimal> Optimals { get; set; }

        public Variation Variation { get; set; }

    }
    

}
