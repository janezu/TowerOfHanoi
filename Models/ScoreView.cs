using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace TowerOfHanoi.Models
    {
        public class ScoreView
        {
            public IEnumerable<Score> Scores { get; set; }
            
            public virtual IdentityUser IdentityUser { get; set; }
            public virtual Configuration Configuration { get; set; }
            public virtual Optimal Optimal { get; set; }
            public virtual Variation Variation { get; set; }
            public string Username { get; set; }
        }
    }
