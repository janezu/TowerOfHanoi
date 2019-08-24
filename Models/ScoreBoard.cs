using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TowerOfHanoi.Models
{
    public class ScoreBoard
    {
        public List<Score> Scores { get; set; }
        public SelectList Variations { get; set; }
        public SelectList Configurations { get; set; }
        public SelectList Optimals { get; set; }
        public int Var { get; set; }
        public string SearchString { get; set; }
        public int Con { get; set; }
        public int diskFilter { get; set; }
        public IdentityUser IdentityUser { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{hh:mm:ss.ff}")]
        public TimeSpan Elapsed { get; set; }

        public int Moves { get; set; }
       
      
        public Optimal Optimal { get; set; }
       
        public Configuration Configuration { get; set; }
      
        public Variation Variation { get; set; }
        public string Username { get; set; }
    }
}
