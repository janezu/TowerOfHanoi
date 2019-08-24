using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerOfHanoi.Models;
using Microsoft.IdentityModel;
using TowerOfHanoi.Data;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace TowerOfHanoi.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //context.Database.EnsureCreated();

           
            if (context.Variation.Any())
            {
              
                return;   // DB has been seeded
            }

           

            var variations = new Variation[]
           {
            new Variation {VariationID=1,TowerN=3 , Connections="{ \"0\": \"1,2\", \"1\": \"0,2\", \"2\": \"0,1\" }", VarPic="/images/3-K3.png", Code="K 3"},
            new Variation {VariationID=2, TowerN=4 , Connections="{\"0\":\"1,2,3\", \"1\":\"0,2,3\", \"2\":\"0,1,3\", \"3\":\"0,1,2\"}", VarPic="/images/4-K4.png", Code="K 4"},
            new Variation {VariationID=3,TowerN=4 , Connections="{\"0\":\"1,2,3\", \"1\":\"0\", \"2\":\"0,3\", \"3\":\"0, 2\"}", VarPic="/images/4-K13e.png", Code="K 1,3 + e"},
            new Variation {VariationID=4,TowerN=4 , Connections="{\"0\": \"2, 3\", \"1\": \"2, 3\", \"2\": \"0, 1\", \"3\": \"0, 1\"}", VarPic="/images/4-C4.png", Code="C 4"},
            new Variation {VariationID=5,TowerN=4 , Connections="{\"0\":\"3\", \"1\":\"2\", \"2\":\"1,3\", \"3\":\"2,0\"}", VarPic="/images/4-P4.png", Code="P 4"},
            new Variation {VariationID=6,TowerN=4 , Connections="{ \"0\":\"1,2,3\", \"1\":\"0\", \"2\":\"0\", \"3\":\"0\"}", VarPic="/images/4-K13.png", Code="K 1,3"},
            new Variation {VariationID=7,TowerN=4 , Connections="{\"0\": \"1,2,3\", \"1\": \"0,2,3\", \"2\": \"0, 1\", \"3\": \"0, 1\"}", VarPic="/images/4-K4e.png", Code="K 4 - e"},
            new Variation {VariationID=8,TowerN=5 , Connections="{ \"0\":\"1,2,3,4\", \"1\":\"0,2,3,4\", \"2\":\"0,1,3,4\", \"3\":\"0,1,2,4\", \"4\":\"0,1,2,3\"}", VarPic="/images/5-K5.png", Code="K 5"},
            new Variation {VariationID=9,TowerN=5 , Connections="{ \"0\":\"1,2,3\", \"1\":\"0\", \"2\":\"0\", \"3\":\"0,4\", \"4\":\"3\"}", VarPic="/images/5-F5.png", Code="F 5"},
            new Variation {VariationID=10,TowerN=3 , Connections="{ \"0\":\"1\", \"1\":\"0,2\", \"2\":\"1\"}", VarPic="/images/3-P3.png", Code="P 3"},

            

           };
            foreach (Variation v in variations)
            {
                context.Variation.Add(v);
            }
            context.SaveChanges();


            var configurations = new Configuration[]
           {
            new Configuration {ConfigurationID = 1, VariationID=1, start=1, end=2, navodilo="From tower 1 to tower 2", conPic="/images/3-K3_12.png"}, //3k


             new Configuration {ConfigurationID = 30, VariationID=10, start=1, end=3, navodilo="From tower 1 to tower 2", conPic="/images/3-P3_12.png"}, //3k

             new Configuration {ConfigurationID = 31, VariationID=10, start=2, end=3, navodilo="From tower 1 to tower 3", conPic="/images/3-P3_13.png"}, //3k

            new Configuration {ConfigurationID = 2, VariationID=2, start=1, end=2, navodilo="From tower 1 to tower 2", conPic="/images/4-K4_12.png"}, //4k

            new Configuration {ConfigurationID = 3, VariationID=3, start=1, end=2, navodilo="From tower 1 to tower 2", conPic="/images/4-K13e_12.png"}, //3a
             new Configuration {ConfigurationID = 7, VariationID=3, start=4, end=1, navodilo="From tower 1 to tower 3", conPic="/images/4-K13e_13.png"}, //3a
            new Configuration {ConfigurationID = 5, VariationID=3, start=2, end=3, navodilo="From tower 2 to tower 3", conPic="/images/4-K13e_23.png"}, //3a
           
            new Configuration {ConfigurationID = 6, VariationID=3, start=3, end=4, navodilo="From tower 3 to tower 4", conPic="/images/4-K13e_34.png"}, //3a
           

            new Configuration {ConfigurationID = 8, VariationID=6, start=1, end=2, navodilo="From tower 1 to tower 2", conPic="/images/4-K13_12.png"}, //k13
            new Configuration {ConfigurationID = 9, VariationID=6, start=2, end=3, navodilo="From tower 2 to tower 3" , conPic="/images/4-K13_23.png" }, //k13

            new Configuration {ConfigurationID = 10, VariationID=4, start=1, end=3, navodilo="From tower 1 to tower 3", conPic="/images/4-C4_13.png" }, //c4
            new Configuration {ConfigurationID = 11, VariationID=4, start=1, end=2 , navodilo="From tower 1 to tower 2", conPic="/images/4-C4_12.png"}, //c4

            new Configuration {ConfigurationID = 12, VariationID=5, start=1, end=2, navodilo="From tower 1 to tower 2", conPic="/images/4-P4_12.png" }, // p4
            new Configuration{ConfigurationID = 13, VariationID=5, start=1, end=4 , navodilo="From tower 1 to tower 4", conPic="/images/4-P4_14.png"}, // p4
            new Configuration {ConfigurationID = 14, VariationID=5, start=3, end=4 , navodilo="From tower 3 to tower 4", conPic="/images/4-P4_34.png"}, //p4
            new Configuration {ConfigurationID = 15, VariationID=5, start=1, end=3 , navodilo="From tower 1 to tower 3", conPic="/images/4-P4_13.png"}, //p4

            new Configuration {ConfigurationID = 16, VariationID=8, start=1, end=2, navodilo="From tower 1 to tower 2", conPic="/images/5-K5_12.png"}, //k5

            new Configuration {ConfigurationID = 17, VariationID=9, start=1, end=2 , navodilo="From tower 1 to tower 2" , conPic="/images/5-F5_12.png"}, //f5
            new Configuration {ConfigurationID = 18, VariationID=9, start=1, end=4 , navodilo="From tower 1 to tower 4", conPic="/images/5-F5_14.png"},
            new Configuration {ConfigurationID = 19, VariationID=9, start=1, end=5 , navodilo="From tower 1 to tower 5", conPic="/images/5-F5_15.png"},
            new Configuration {ConfigurationID = 20, VariationID=9, start=2, end=3 , navodilo="From tower 2 to tower 3", conPic="/images/5-F5_23.png"},
            new Configuration {ConfigurationID = 21, VariationID=9, start=2, end=4 , navodilo="From tower 2 to tower 4", conPic="/images/5-F5_24.png"},
            new Configuration {ConfigurationID = 22, VariationID=9, start=2, end=5 , navodilo="From tower 2 to tower 5", conPic="/images/5-F5_25.png"},
            new Configuration {ConfigurationID = 23, VariationID=9, start=4, end=5 , navodilo="From tower 4 to tower 5", conPic="/images/5-F5_45.png"},

            new Configuration {ConfigurationID = 24, VariationID=7, start=1, end=2, navodilo="From tower 1 to tower 2" , conPic="/images/4-K4e_12.png"},
            new Configuration {ConfigurationID = 25, VariationID=7, start=1, end=3, navodilo="From tower 1 to tower 3", conPic="/images/4-K4e_13.png" },
            new Configuration {ConfigurationID = 26, VariationID=7, start=3, end=4 , navodilo="From tower 3 to tower 4", conPic="/images/4-K4e_34.png"},





           };
            foreach (Configuration c in configurations)
            {
                context.Configuration.Add(c);
            }
            context.SaveChanges();

            var Optimals = new Optimal[]

          {
            new Optimal {ConfigurationID=1, DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=1, DiskNr=2, MovesO=3},
            new Optimal {ConfigurationID=1, DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=1, DiskNr=4, MovesO=15},
            new Optimal {ConfigurationID=1, DiskNr=5, MovesO=31},
            new Optimal {ConfigurationID=1, DiskNr=6, MovesO=63},
            new Optimal {ConfigurationID=1, DiskNr=7, MovesO=127},
            new Optimal {ConfigurationID=1,DiskNr=8, MovesO=255},

            new Optimal {ConfigurationID=2,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=2,DiskNr=2, MovesO=3},
            new Optimal {ConfigurationID=2,DiskNr=3, MovesO=5},
            new Optimal {ConfigurationID=2,DiskNr=4, MovesO=9},
            new Optimal {ConfigurationID=2,DiskNr=5, MovesO=13},
            new Optimal {ConfigurationID=2,DiskNr=6, MovesO=17},
            new Optimal {ConfigurationID=2,DiskNr=7, MovesO=25},
            new Optimal {ConfigurationID=2,DiskNr=8, MovesO=33},

            new Optimal {ConfigurationID=3,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=3,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=3,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=3,DiskNr=4, MovesO=12},
            new Optimal {ConfigurationID=3,DiskNr=5, MovesO=20},
            new Optimal {ConfigurationID=3,DiskNr=6, MovesO=29},
            new Optimal {ConfigurationID=3,DiskNr=7, MovesO=39},
            new Optimal {ConfigurationID=3,DiskNr=8, MovesO=54},
            new Optimal {ConfigurationID=3,DiskNr=9, MovesO=72},

            new Optimal {ConfigurationID=5,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=5,DiskNr=2, MovesO=5},
            new Optimal {ConfigurationID=5,DiskNr=3, MovesO=10},
            new Optimal {ConfigurationID=5,DiskNr=4, MovesO=16},
            new Optimal {ConfigurationID=5,DiskNr=5, MovesO=25},
            new Optimal {ConfigurationID=5,DiskNr=6, MovesO=35},
            new Optimal {ConfigurationID=5,DiskNr=7, MovesO=50},
            new Optimal {ConfigurationID=5,DiskNr=8, MovesO=68},
            new Optimal {ConfigurationID=5,DiskNr=9, MovesO=86},

            new Optimal {ConfigurationID=6,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=6,DiskNr=2, MovesO=3},
            new Optimal {ConfigurationID=6,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=6,DiskNr=4, MovesO=11},
            new Optimal {ConfigurationID=6,DiskNr=5, MovesO=17},
            new Optimal {ConfigurationID=6,DiskNr=6, MovesO=25},
            new Optimal {ConfigurationID=6,DiskNr=7, MovesO=35},
            new Optimal {ConfigurationID=6,DiskNr=8, MovesO=47},
            new Optimal {ConfigurationID=6,DiskNr=9, MovesO=63},

            new Optimal {ConfigurationID=7,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=7,DiskNr=2, MovesO=3},
            new Optimal {ConfigurationID=7,DiskNr=3, MovesO=6},
            new Optimal {ConfigurationID=7,DiskNr=4, MovesO=10},
            new Optimal {ConfigurationID=7,DiskNr=5, MovesO=16},
            new Optimal {ConfigurationID=7,DiskNr=6, MovesO=24},
            new Optimal {ConfigurationID=7,DiskNr=7, MovesO=32},
            new Optimal {ConfigurationID=7,DiskNr=8, MovesO=43},
            new Optimal {ConfigurationID=7,DiskNr=9, MovesO=59},

            new Optimal {ConfigurationID=8,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=8,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=8,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=8,DiskNr=4, MovesO=14},
            new Optimal {ConfigurationID=8,DiskNr=5, MovesO=23},
            new Optimal {ConfigurationID=8,DiskNr=6, MovesO=32},
            new Optimal {ConfigurationID=8,DiskNr=7, MovesO=47},
            new Optimal {ConfigurationID=8,DiskNr=8, MovesO=68},
            new Optimal {ConfigurationID=8,DiskNr=9, MovesO=93},

             new Optimal {ConfigurationID=9,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=9,DiskNr=2, MovesO=6},
            new Optimal {ConfigurationID=9,DiskNr=3, MovesO=12},
            new Optimal {ConfigurationID=9,DiskNr=4, MovesO=20},
            new Optimal {ConfigurationID=9,DiskNr=5, MovesO=32},
            new Optimal {ConfigurationID=9,DiskNr=6, MovesO=48},
            new Optimal {ConfigurationID=9,DiskNr=7, MovesO=66},
            new Optimal {ConfigurationID=9,DiskNr=8, MovesO=90},
            new Optimal {ConfigurationID=9,DiskNr=9, MovesO=122},

             new Optimal {ConfigurationID=24,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=24,DiskNr=2, MovesO=3},
            new Optimal {ConfigurationID=24,DiskNr=3, MovesO=5},
            new Optimal {ConfigurationID=24,DiskNr=4, MovesO=9},
            new Optimal {ConfigurationID=24,DiskNr=5, MovesO=13},
            new Optimal {ConfigurationID=24,DiskNr=6, MovesO=19},
            new Optimal {ConfigurationID=24,DiskNr=7, MovesO=27},
            new Optimal {ConfigurationID=24,DiskNr=8, MovesO=35},
            new Optimal {ConfigurationID=24,DiskNr=9, MovesO=43},

             new Optimal {ConfigurationID=25,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=25,DiskNr=2, MovesO=3},
            new Optimal {ConfigurationID=25,DiskNr=3, MovesO=6},
            new Optimal {ConfigurationID=25,DiskNr=4, MovesO=10},
            new Optimal {ConfigurationID=25,DiskNr=5, MovesO=14},
            new Optimal {ConfigurationID=25,DiskNr=6, MovesO=21},
            new Optimal {ConfigurationID=25,DiskNr=7, MovesO=29},
            new Optimal {ConfigurationID=25,DiskNr=8, MovesO=39},
            new Optimal {ConfigurationID=25,DiskNr=9, MovesO=49},

             new Optimal {ConfigurationID=26,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=26,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=26,DiskNr=3, MovesO=8},
            new Optimal {ConfigurationID=26,DiskNr=4, MovesO=14},
            new Optimal {ConfigurationID=26,DiskNr=5, MovesO=20},
            new Optimal {ConfigurationID=26,DiskNr=6, MovesO=28},
            new Optimal {ConfigurationID=26,DiskNr=7, MovesO=36},
            new Optimal {ConfigurationID=26,DiskNr=8, MovesO=50},
            new Optimal {ConfigurationID=26,DiskNr=9, MovesO=66},

             new Optimal {ConfigurationID=10,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=10,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=10,DiskNr=3, MovesO=10},
            new Optimal {ConfigurationID=10,DiskNr=4, MovesO=16},
            new Optimal {ConfigurationID=10,DiskNr=5, MovesO=22},
            new Optimal {ConfigurationID=10,DiskNr=6, MovesO=32},
            new Optimal {ConfigurationID=10,DiskNr=7, MovesO=50},
            new Optimal {ConfigurationID=10,DiskNr=8, MovesO=68},
            new Optimal {ConfigurationID=10,DiskNr=9, MovesO=86},

            new Optimal {ConfigurationID=11,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=11,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=11,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=11,DiskNr=4, MovesO=12},
            new Optimal {ConfigurationID=11,DiskNr=5, MovesO=21},
            new Optimal {ConfigurationID=11,DiskNr=6, MovesO=30},
            new Optimal {ConfigurationID=11,DiskNr=7, MovesO=41},
            new Optimal {ConfigurationID=11,DiskNr=8, MovesO=56},
            new Optimal {ConfigurationID=11,DiskNr=9, MovesO=75},

            new Optimal {ConfigurationID=12,DiskNr=1, MovesO=3},
            new Optimal {ConfigurationID=12,DiskNr=2, MovesO=10},
            new Optimal {ConfigurationID=12,DiskNr=3, MovesO=19},
            new Optimal {ConfigurationID=12,DiskNr=4, MovesO=34},
            new Optimal {ConfigurationID=12,DiskNr=5, MovesO=57},
            new Optimal {ConfigurationID=12,DiskNr=6, MovesO=88},
            new Optimal {ConfigurationID=12,DiskNr=7, MovesO=123},
            new Optimal {ConfigurationID=12,DiskNr=8, MovesO=176},
            new Optimal {ConfigurationID=12,DiskNr=9, MovesO=253},

            new Optimal {ConfigurationID=13,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=13,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=13,DiskNr=3, MovesO=9},
            new Optimal {ConfigurationID=13,DiskNr=4, MovesO=18},
            new Optimal {ConfigurationID=13,DiskNr=5, MovesO=29},
            new Optimal {ConfigurationID=13,DiskNr=6, MovesO=44},
            new Optimal {ConfigurationID=13,DiskNr=7, MovesO=69},
            new Optimal {ConfigurationID=13,DiskNr=8, MovesO=96},
            new Optimal {ConfigurationID=13,DiskNr=9, MovesO=133},

            new Optimal {ConfigurationID=14,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=14,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=14,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=14,DiskNr=4, MovesO=14},
            new Optimal {ConfigurationID=14,DiskNr=5, MovesO=23},
            new Optimal {ConfigurationID=14,DiskNr=6, MovesO=34},
            new Optimal {ConfigurationID=14,DiskNr=7, MovesO=53},
            new Optimal {ConfigurationID=14,DiskNr=8, MovesO=78},
            new Optimal {ConfigurationID=14,DiskNr=9, MovesO=105},

            new Optimal {ConfigurationID=15,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=15,DiskNr=2, MovesO=6},
            new Optimal {ConfigurationID=15,DiskNr=3, MovesO=12},
            new Optimal {ConfigurationID=15,DiskNr=4, MovesO=22},
            new Optimal {ConfigurationID=15,DiskNr=5, MovesO=36},
            new Optimal {ConfigurationID=15,DiskNr=6, MovesO=54},
            new Optimal {ConfigurationID=15,DiskNr=7, MovesO=78},
            new Optimal {ConfigurationID=15,DiskNr=8, MovesO=112},
            new Optimal {ConfigurationID=15,DiskNr=9, MovesO=158},

            new Optimal {ConfigurationID=16,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=16,DiskNr=2, MovesO=3},
            new Optimal {ConfigurationID=16,DiskNr=3, MovesO=5},
            new Optimal {ConfigurationID=16,DiskNr=4, MovesO=0},
            new Optimal {ConfigurationID=16,DiskNr=5, MovesO=0},
            new Optimal {ConfigurationID=16,DiskNr=6, MovesO=0},
            new Optimal {ConfigurationID=16,DiskNr=7, MovesO=0},
            new Optimal {ConfigurationID=16,DiskNr=8, MovesO=0},
            new Optimal {ConfigurationID=16,DiskNr=9, MovesO=0},

            new Optimal {ConfigurationID=17,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=17,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=17,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=17,DiskNr=4, MovesO=12},
            new Optimal {ConfigurationID=17,DiskNr=5, MovesO=19},
            new Optimal {ConfigurationID=17,DiskNr=6, MovesO=28},
            new Optimal {ConfigurationID=17,DiskNr=7, MovesO=37},
            new Optimal {ConfigurationID=17,DiskNr=8, MovesO=46},
            new Optimal {ConfigurationID=17,DiskNr=9, MovesO=57},

             new Optimal {ConfigurationID=18,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=18,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=18,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=18,DiskNr=4, MovesO=10},
            new Optimal {ConfigurationID=18,DiskNr=5, MovesO=17},
            new Optimal {ConfigurationID=18,DiskNr=6, MovesO=24},
            new Optimal {ConfigurationID=18,DiskNr=7, MovesO=31},
            new Optimal {ConfigurationID=18,DiskNr=8, MovesO=40},
            new Optimal {ConfigurationID=18,DiskNr=9, MovesO=51},

             new Optimal {ConfigurationID=19,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=19,DiskNr=2, MovesO=6},
            new Optimal {ConfigurationID=19,DiskNr=3, MovesO=10},
            new Optimal {ConfigurationID=19,DiskNr=4, MovesO=16},
            new Optimal {ConfigurationID=19,DiskNr=5, MovesO=24},
            new Optimal {ConfigurationID=19,DiskNr=6, MovesO=34},
            new Optimal {ConfigurationID=19,DiskNr=7, MovesO=44},
            new Optimal {ConfigurationID=19,DiskNr=8, MovesO=58},
            new Optimal {ConfigurationID=19,DiskNr=9, MovesO=72},

             new Optimal {ConfigurationID=20,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=20,DiskNr=2, MovesO=6},
            new Optimal {ConfigurationID=20,DiskNr=3, MovesO=12},
            new Optimal {ConfigurationID=20,DiskNr=4, MovesO=18},
            new Optimal {ConfigurationID=20,DiskNr=5, MovesO=26},
            new Optimal {ConfigurationID=20,DiskNr=6, MovesO=36},
            new Optimal {ConfigurationID=20,DiskNr=7, MovesO=48},
            new Optimal {ConfigurationID=20,DiskNr=8, MovesO=62},
            new Optimal {ConfigurationID=20,DiskNr=9, MovesO=78},

             new Optimal {ConfigurationID=21,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=21,DiskNr=2, MovesO=6},
            new Optimal {ConfigurationID=21,DiskNr=3, MovesO=10},
            new Optimal {ConfigurationID=21,DiskNr=4, MovesO=16},
            new Optimal {ConfigurationID=21,DiskNr=5, MovesO=24},
            new Optimal {ConfigurationID=21,DiskNr=6, MovesO=32},
            new Optimal {ConfigurationID=21,DiskNr=7, MovesO=44},
            new Optimal {ConfigurationID=21,DiskNr=8, MovesO=56},
            new Optimal {ConfigurationID=21,DiskNr=9, MovesO=70},

             new Optimal {ConfigurationID=22,DiskNr=1, MovesO=3},
            new Optimal {ConfigurationID=22,DiskNr=2, MovesO=8},
            new Optimal {ConfigurationID=22,DiskNr=3, MovesO=15},
            new Optimal {ConfigurationID=22,DiskNr=4, MovesO=24},
            new Optimal {ConfigurationID=22,DiskNr=5, MovesO=33},
            new Optimal {ConfigurationID=22,DiskNr=6, MovesO=46},
            new Optimal {ConfigurationID=22,DiskNr=7, MovesO=61},
            new Optimal {ConfigurationID=22,DiskNr=8, MovesO=76},
            new Optimal {ConfigurationID=22,DiskNr=9, MovesO=93},

             new Optimal {ConfigurationID=23,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=23,DiskNr=2, MovesO=4 },
            new Optimal {ConfigurationID=23,DiskNr=3, MovesO=9},
            new Optimal {ConfigurationID=23,DiskNr=4, MovesO=14},
            new Optimal {ConfigurationID=23,DiskNr=5, MovesO=23},
            new Optimal {ConfigurationID=23,DiskNr=6, MovesO=32},
            new Optimal {ConfigurationID=23,DiskNr=7, MovesO=43},
            new Optimal {ConfigurationID=23,DiskNr=8, MovesO=54},
            new Optimal {ConfigurationID=23,DiskNr=9, MovesO=69},

             new Optimal {ConfigurationID=30,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=30,DiskNr=2, MovesO=0 },
            new Optimal {ConfigurationID=30,DiskNr=3, MovesO=0},
            new Optimal {ConfigurationID=30,DiskNr=4, MovesO=0},
            new Optimal {ConfigurationID=30,DiskNr=5, MovesO=0},
            new Optimal {ConfigurationID=30,DiskNr=6, MovesO=0},
            new Optimal {ConfigurationID=30,DiskNr=7, MovesO=0},
            new Optimal {ConfigurationID=30,DiskNr=8, MovesO=0},
            new Optimal {ConfigurationID=30,DiskNr=9, MovesO=0},

            new Optimal {ConfigurationID=31,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=31,DiskNr=2, MovesO=0 },
            new Optimal {ConfigurationID=31,DiskNr=3, MovesO=0},
            new Optimal {ConfigurationID=31,DiskNr=4, MovesO=0},
            new Optimal {ConfigurationID=31,DiskNr=5, MovesO=0},
            new Optimal {ConfigurationID=31,DiskNr=6, MovesO=0},
            new Optimal {ConfigurationID=31,DiskNr=7, MovesO=0},
            new Optimal {ConfigurationID=31,DiskNr=8, MovesO=0},
            new Optimal {ConfigurationID=31,DiskNr=9, MovesO=0},


          };
            foreach (Optimal o in Optimals)
            {
                context.Optimal.Add(o);
            }
            context.SaveChanges();


            
         

        

        }


    }
}
