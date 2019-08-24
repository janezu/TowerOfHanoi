using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TowerOfHanoi.Models
{
    public class Optimal
    {
        public int OptimalID { get; set; }
        public int DiskNr { get; set; }
        public int MovesO { get; set; }
        public int ConfigurationID { get; set; }

        public virtual Configuration Configuration {get; set;}

    }

}
