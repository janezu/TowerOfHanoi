using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using TowerOfHanoi.Data;
using Microsoft.AspNetCore.Identity;

namespace TowerOfHanoi.Models
{
    public class Score
    {
        public int ScoreID { get; set; }
        public int Moves { get; set; }
        public string IdentityUserID { get; set; }
        public int OptimalID { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{hh:mm:ss.ff}")]
        public TimeSpan Elapsed { get; set; }

        public virtual IdentityUser IdentityUser { get; set; }
        public virtual Optimal Optimal { get; set; }

    }

    public class ScoreTable
    {
        public int ScoreID { get; set; }
        public int Moves { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{hh:mm:ss.ff}")]
        public TimeSpan Elapsed { get; set; }
        public string UserName { get; set; }
    }
}
