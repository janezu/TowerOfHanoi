using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TowerOfHanoi.Models
    {
        public class ScoreIndexData
        {
            public IEnumerable<Score> Scores { get; set; }
            public IEnumerable<Variation> Variations { get; set; }
        public IEnumerable<Optimal> Optimals { get; set; }
        public IEnumerable<IdentityUser> IdentityUsers { get; set; }
        //public IEnumerable<AspNetUser> AspNetUsers { get; set; }
        public IEnumerable<Configuration> Configurations { get; set; }



        public List<int> unique { get; set;}
        public int OptimalID { get; set; }
        public int ScoreID { get; set; }
        public int Moves { get; set; }
        public string IdentityUserID { get; set; }
        public int ConfigurationID { get; set; }
        //[DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{hh:mm:ss.ff}")]
        public TimeSpan Elapsed { get; set; }
        public bool ShowHiddenLinks { get; set; }

       




    }
}


