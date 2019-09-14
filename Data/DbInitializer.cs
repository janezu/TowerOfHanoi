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
            new Optimal {ConfigurationID=1,DiskNr=9, MovesO=511},
            new Optimal {ConfigurationID=1,DiskNr=10, MovesO=1023},
            new Optimal {ConfigurationID=1,DiskNr=11, MovesO=2047},
            new Optimal {ConfigurationID=1,DiskNr=12, MovesO=4095},
            new Optimal {ConfigurationID=1,DiskNr=13, MovesO=8191},
            new Optimal {ConfigurationID=1,DiskNr=14, MovesO=16383},
            new Optimal {ConfigurationID=1,DiskNr=15, MovesO=32767},
            

            new Optimal {ConfigurationID=2,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=2,DiskNr=2, MovesO=3},
            new Optimal {ConfigurationID=2,DiskNr=3, MovesO=5},
            new Optimal {ConfigurationID=2,DiskNr=4, MovesO=9},
            new Optimal {ConfigurationID=2,DiskNr=5, MovesO=13},
            new Optimal {ConfigurationID=2,DiskNr=6, MovesO=17},
            new Optimal {ConfigurationID=2,DiskNr=7, MovesO=25},
            new Optimal {ConfigurationID=2,DiskNr=8, MovesO=33},
            new Optimal {ConfigurationID=2,DiskNr=9, MovesO=41},
            new Optimal {ConfigurationID=2,DiskNr=10, MovesO=49},
            new Optimal {ConfigurationID=2,DiskNr=11, MovesO=65},
            new Optimal {ConfigurationID=2,DiskNr=12, MovesO=81},
            new Optimal {ConfigurationID=2,DiskNr=13, MovesO=97},
            new Optimal {ConfigurationID=2,DiskNr=14, MovesO=113},
            new Optimal {ConfigurationID=2,DiskNr=15, MovesO=129},

            new Optimal {ConfigurationID=3,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=3,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=3,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=3,DiskNr=4, MovesO=12},
            new Optimal {ConfigurationID=3,DiskNr=5, MovesO=20},
            new Optimal {ConfigurationID=3,DiskNr=6, MovesO=29},
            new Optimal {ConfigurationID=3,DiskNr=7, MovesO=39},
            new Optimal {ConfigurationID=3,DiskNr=8, MovesO=54},
            new Optimal {ConfigurationID=3,DiskNr=9, MovesO=72},
            new Optimal {ConfigurationID=3,DiskNr=10, MovesO=95},
            new Optimal {ConfigurationID=3,DiskNr=11, MovesO=122},
            new Optimal {ConfigurationID=3,DiskNr=12, MovesO=151},
            new Optimal {ConfigurationID=3,DiskNr=13, MovesO=185},
            new Optimal {ConfigurationID=3,DiskNr=14, MovesO=227},
            new Optimal {ConfigurationID=3,DiskNr=15, MovesO=276},
        

            new Optimal {ConfigurationID=5,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=5,DiskNr=2, MovesO=5},
            new Optimal {ConfigurationID=5,DiskNr=3, MovesO=10},
            new Optimal {ConfigurationID=5,DiskNr=4, MovesO=16},
            new Optimal {ConfigurationID=5,DiskNr=5, MovesO=25},
            new Optimal {ConfigurationID=5,DiskNr=6, MovesO=35},
            new Optimal {ConfigurationID=5,DiskNr=7, MovesO=50},
            new Optimal {ConfigurationID=5,DiskNr=8, MovesO=68},
            new Optimal {ConfigurationID=5,DiskNr=9, MovesO=86},
            new Optimal {ConfigurationID=5,DiskNr=10, MovesO=111},
            new Optimal {ConfigurationID=5,DiskNr=11, MovesO=141},
            new Optimal {ConfigurationID=5,DiskNr=12, MovesO=175},
            new Optimal {ConfigurationID=5,DiskNr=13, MovesO=218},
            new Optimal {ConfigurationID=5,DiskNr=14, MovesO=268},
            new Optimal {ConfigurationID=5,DiskNr=15, MovesO=322},

            new Optimal {ConfigurationID=6,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=6,DiskNr=2, MovesO=3},
            new Optimal {ConfigurationID=6,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=6,DiskNr=4, MovesO=11},
            new Optimal {ConfigurationID=6,DiskNr=5, MovesO=17},
            new Optimal {ConfigurationID=6,DiskNr=6, MovesO=25},
            new Optimal {ConfigurationID=6,DiskNr=7, MovesO=35},
            new Optimal {ConfigurationID=6,DiskNr=8, MovesO=47},
            new Optimal {ConfigurationID=6,DiskNr=9, MovesO=63},
            new Optimal {ConfigurationID=6,DiskNr=10, MovesO=81},
            new Optimal {ConfigurationID=6,DiskNr=11, MovesO=101},
            new Optimal {ConfigurationID=6,DiskNr=12, MovesO=131},
            new Optimal {ConfigurationID=6,DiskNr=13, MovesO=163},
            new Optimal {ConfigurationID=6,DiskNr=14, MovesO=199},
            new Optimal {ConfigurationID=6,DiskNr=15, MovesO=235},

            new Optimal {ConfigurationID=7,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=7,DiskNr=2, MovesO=3},
            new Optimal {ConfigurationID=7,DiskNr=3, MovesO=6},
            new Optimal {ConfigurationID=7,DiskNr=4, MovesO=10},
            new Optimal {ConfigurationID=7,DiskNr=5, MovesO=16},
            new Optimal {ConfigurationID=7,DiskNr=6, MovesO=24},
            new Optimal {ConfigurationID=7,DiskNr=7, MovesO=32},
            new Optimal {ConfigurationID=7,DiskNr=8, MovesO=43},
            new Optimal {ConfigurationID=7,DiskNr=9, MovesO=59},
            new Optimal {ConfigurationID=7,DiskNr=10, MovesO=76},
            new Optimal {ConfigurationID=7,DiskNr=11, MovesO=95},
            new Optimal {ConfigurationID=7,DiskNr=12, MovesO=120},
            new Optimal {ConfigurationID=7,DiskNr=13, MovesO=152},
            new Optimal {ConfigurationID=7,DiskNr=14, MovesO=185},
            new Optimal {ConfigurationID=7,DiskNr=15, MovesO=221},

            new Optimal {ConfigurationID=8,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=8,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=8,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=8,DiskNr=4, MovesO=14},
            new Optimal {ConfigurationID=8,DiskNr=5, MovesO=23},
            new Optimal {ConfigurationID=8,DiskNr=6, MovesO=32},
            new Optimal {ConfigurationID=8,DiskNr=7, MovesO=47},
            new Optimal {ConfigurationID=8,DiskNr=8, MovesO=68},
            new Optimal {ConfigurationID=8,DiskNr=9, MovesO=93},
            new Optimal {ConfigurationID=8,DiskNr=10, MovesO=120},
            new Optimal {ConfigurationID=8,DiskNr=11, MovesO=153},
            new Optimal {ConfigurationID=8,DiskNr=12, MovesO=198},
            new Optimal {ConfigurationID=8,DiskNr=13, MovesO=255},
            new Optimal {ConfigurationID=8,DiskNr=14, MovesO=318},
            new Optimal {ConfigurationID=8,DiskNr=15, MovesO=399},

            new Optimal {ConfigurationID=9,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=9,DiskNr=2, MovesO=6},
            new Optimal {ConfigurationID=9,DiskNr=3, MovesO=12},
            new Optimal {ConfigurationID=9,DiskNr=4, MovesO=20},
            new Optimal {ConfigurationID=9,DiskNr=5, MovesO=32},
            new Optimal {ConfigurationID=9,DiskNr=6, MovesO=48},
            new Optimal {ConfigurationID=9,DiskNr=7, MovesO=66},
            new Optimal {ConfigurationID=9,DiskNr=8, MovesO=90},
            new Optimal {ConfigurationID=9,DiskNr=9, MovesO=122},
             new Optimal {ConfigurationID=9,DiskNr=10, MovesO=158},
            new Optimal {ConfigurationID=9,DiskNr=11, MovesO=206},
            new Optimal {ConfigurationID=9,DiskNr=12, MovesO=260},
            new Optimal {ConfigurationID=9,DiskNr=13, MovesO=324},
            new Optimal {ConfigurationID=9,DiskNr=14, MovesO=396},
            new Optimal {ConfigurationID=9,DiskNr=15, MovesO=492},

             new Optimal {ConfigurationID=24,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=24,DiskNr=2, MovesO=3},
            new Optimal {ConfigurationID=24,DiskNr=3, MovesO=5},
            new Optimal {ConfigurationID=24,DiskNr=4, MovesO=9},
            new Optimal {ConfigurationID=24,DiskNr=5, MovesO=13},
            new Optimal {ConfigurationID=24,DiskNr=6, MovesO=19},
            new Optimal {ConfigurationID=24,DiskNr=7, MovesO=27},
            new Optimal {ConfigurationID=24,DiskNr=8, MovesO=35},
            new Optimal {ConfigurationID=24,DiskNr=9, MovesO=43},
             new Optimal {ConfigurationID=24,DiskNr=10, MovesO=57},
            new Optimal {ConfigurationID=24,DiskNr=11, MovesO=73},
            new Optimal {ConfigurationID=24,DiskNr=12, MovesO=89},
            new Optimal {ConfigurationID=24,DiskNr=13, MovesO=109},
            new Optimal {ConfigurationID=24,DiskNr=14, MovesO=129},
            new Optimal {ConfigurationID=24,DiskNr=15, MovesO=159},

             new Optimal {ConfigurationID=25,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=25,DiskNr=2, MovesO=3},
            new Optimal {ConfigurationID=25,DiskNr=3, MovesO=6},
            new Optimal {ConfigurationID=25,DiskNr=4, MovesO=10},
            new Optimal {ConfigurationID=25,DiskNr=5, MovesO=14},
            new Optimal {ConfigurationID=25,DiskNr=6, MovesO=21},
            new Optimal {ConfigurationID=25,DiskNr=7, MovesO=29},
            new Optimal {ConfigurationID=25,DiskNr=8, MovesO=39},
            new Optimal {ConfigurationID=25,DiskNr=9, MovesO=49},
            new Optimal {ConfigurationID=25,DiskNr=10, MovesO=64},
            new Optimal {ConfigurationID=25,DiskNr=11, MovesO=80},
            new Optimal {ConfigurationID=25,DiskNr=12, MovesO=96},
            new Optimal {ConfigurationID=25,DiskNr=13, MovesO=120},
            new Optimal {ConfigurationID=25,DiskNr=14, MovesO=146},
            new Optimal {ConfigurationID=25,DiskNr=15, MovesO=178},

             new Optimal {ConfigurationID=26,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=26,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=26,DiskNr=3, MovesO=8},
            new Optimal {ConfigurationID=26,DiskNr=4, MovesO=14},
            new Optimal {ConfigurationID=26,DiskNr=5, MovesO=20},
            new Optimal {ConfigurationID=26,DiskNr=6, MovesO=28},
            new Optimal {ConfigurationID=26,DiskNr=7, MovesO=36},
            new Optimal {ConfigurationID=26,DiskNr=8, MovesO=50},
            new Optimal {ConfigurationID=26,DiskNr=9, MovesO=66},
            new Optimal {ConfigurationID=26,DiskNr=10, MovesO=84},
            new Optimal {ConfigurationID=26,DiskNr=11, MovesO=104},
            new Optimal {ConfigurationID=26,DiskNr=12, MovesO=124},
            new Optimal {ConfigurationID=26,DiskNr=13, MovesO=154},
            new Optimal {ConfigurationID=26,DiskNr=14, MovesO=186},
            new Optimal {ConfigurationID=26,DiskNr=15, MovesO=218},

             new Optimal {ConfigurationID=10,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=10,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=10,DiskNr=3, MovesO=10},
            new Optimal {ConfigurationID=10,DiskNr=4, MovesO=16},
            new Optimal {ConfigurationID=10,DiskNr=5, MovesO=22},
            new Optimal {ConfigurationID=10,DiskNr=6, MovesO=32},
            new Optimal {ConfigurationID=10,DiskNr=7, MovesO=50},
            new Optimal {ConfigurationID=10,DiskNr=8, MovesO=68},
            new Optimal {ConfigurationID=10,DiskNr=9, MovesO=86},
            new Optimal {ConfigurationID=10,DiskNr=10, MovesO=108},
            new Optimal {ConfigurationID=10,DiskNr=11, MovesO=138},
            new Optimal {ConfigurationID=10,DiskNr=12, MovesO=176},
            new Optimal {ConfigurationID=10,DiskNr=13, MovesO=230},
            new Optimal {ConfigurationID=10,DiskNr=14, MovesO=284},
            new Optimal {ConfigurationID=10,DiskNr=15, MovesO=342},

            new Optimal {ConfigurationID=11,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=11,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=11,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=11,DiskNr=4, MovesO=12},
            new Optimal {ConfigurationID=11,DiskNr=5, MovesO=21},
            new Optimal {ConfigurationID=11,DiskNr=6, MovesO=30},
            new Optimal {ConfigurationID=11,DiskNr=7, MovesO=41},
            new Optimal {ConfigurationID=11,DiskNr=8, MovesO=56},
            new Optimal {ConfigurationID=11,DiskNr=9, MovesO=75},
             new Optimal {ConfigurationID=11,DiskNr=10, MovesO=102},
            new Optimal {ConfigurationID=11,DiskNr=11, MovesO=131},
            new Optimal {ConfigurationID=11,DiskNr=12, MovesO=164},
            new Optimal {ConfigurationID=11,DiskNr=13, MovesO=201},
            new Optimal {ConfigurationID=11,DiskNr=14, MovesO=250},
            new Optimal {ConfigurationID=11,DiskNr=15, MovesO=309},

            new Optimal {ConfigurationID=12,DiskNr=1, MovesO=3},
            new Optimal {ConfigurationID=12,DiskNr=2, MovesO=10},
            new Optimal {ConfigurationID=12,DiskNr=3, MovesO=19},
            new Optimal {ConfigurationID=12,DiskNr=4, MovesO=34},
            new Optimal {ConfigurationID=12,DiskNr=5, MovesO=57},
            new Optimal {ConfigurationID=12,DiskNr=6, MovesO=88},
            new Optimal {ConfigurationID=12,DiskNr=7, MovesO=123},
            new Optimal {ConfigurationID=12,DiskNr=8, MovesO=176},
            new Optimal {ConfigurationID=12,DiskNr=9, MovesO=253},
             new Optimal {ConfigurationID=12,DiskNr=10, MovesO=342},
            new Optimal {ConfigurationID=12,DiskNr=11, MovesO=449},
            new Optimal {ConfigurationID=12,DiskNr=12, MovesO=572},
            new Optimal {ConfigurationID=12,DiskNr=13, MovesO=749},
            new Optimal {ConfigurationID=12,DiskNr=14, MovesO=980},
            new Optimal {ConfigurationID=12,DiskNr=15, MovesO=1261},

            new Optimal {ConfigurationID=13,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=13,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=13,DiskNr=3, MovesO=9},
            new Optimal {ConfigurationID=13,DiskNr=4, MovesO=18},
            new Optimal {ConfigurationID=13,DiskNr=5, MovesO=29},
            new Optimal {ConfigurationID=13,DiskNr=6, MovesO=44},
            new Optimal {ConfigurationID=13,DiskNr=7, MovesO=69},
            new Optimal {ConfigurationID=13,DiskNr=8, MovesO=96},
            new Optimal {ConfigurationID=13,DiskNr=9, MovesO=133},
            new Optimal {ConfigurationID=13,DiskNr=10, MovesO=182},
            new Optimal {ConfigurationID=13,DiskNr=11, MovesO=241},
            new Optimal {ConfigurationID=13,DiskNr=12, MovesO=322},
            new Optimal {ConfigurationID=13,DiskNr=13, MovesO=409},
            new Optimal {ConfigurationID=13,DiskNr=14, MovesO=532},
            new Optimal {ConfigurationID=13,DiskNr=15, MovesO=675},

            new Optimal {ConfigurationID=14,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=14,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=14,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=14,DiskNr=4, MovesO=14},
            new Optimal {ConfigurationID=14,DiskNr=5, MovesO=23},
            new Optimal {ConfigurationID=14,DiskNr=6, MovesO=34},
            new Optimal {ConfigurationID=14,DiskNr=7, MovesO=53},
            new Optimal {ConfigurationID=14,DiskNr=8, MovesO=78},
            new Optimal {ConfigurationID=14,DiskNr=9, MovesO=105},
             new Optimal {ConfigurationID=14,DiskNr=10, MovesO=138},
            new Optimal {ConfigurationID=14,DiskNr=11, MovesO=187},
            new Optimal {ConfigurationID=14,DiskNr=12, MovesO=248},
            new Optimal {ConfigurationID=14,DiskNr=13, MovesO=329},
            new Optimal {ConfigurationID=14,DiskNr=14, MovesO=412},
            new Optimal {ConfigurationID=14,DiskNr=15, MovesO=515},

            new Optimal {ConfigurationID=15,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=15,DiskNr=2, MovesO=6},
            new Optimal {ConfigurationID=15,DiskNr=3, MovesO=12},
            new Optimal {ConfigurationID=15,DiskNr=4, MovesO=22},
            new Optimal {ConfigurationID=15,DiskNr=5, MovesO=36},
            new Optimal {ConfigurationID=15,DiskNr=6, MovesO=54},
            new Optimal {ConfigurationID=15,DiskNr=7, MovesO=78},
            new Optimal {ConfigurationID=15,DiskNr=8, MovesO=112},
            new Optimal {ConfigurationID=15,DiskNr=9, MovesO=158},
            new Optimal {ConfigurationID=15,DiskNr=10, MovesO=212},
            new Optimal {ConfigurationID=15,DiskNr=11, MovesO=272},
            new Optimal {ConfigurationID=15,DiskNr=12, MovesO=352},
            new Optimal {ConfigurationID=15,DiskNr=13, MovesO=466},
            new Optimal {ConfigurationID=15,DiskNr=14, MovesO=604},
            new Optimal {ConfigurationID=15,DiskNr=15, MovesO=766},

            new Optimal {ConfigurationID=16,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=16,DiskNr=2, MovesO=3},
            new Optimal {ConfigurationID=16,DiskNr=3, MovesO=5},
            new Optimal {ConfigurationID=16,DiskNr=4, MovesO=13},
            new Optimal {ConfigurationID=16,DiskNr=5, MovesO=17},
            new Optimal {ConfigurationID=16,DiskNr=6, MovesO=25},
            new Optimal {ConfigurationID=16,DiskNr=7, MovesO=33},
            new Optimal {ConfigurationID=16,DiskNr=8, MovesO=41},
            new Optimal {ConfigurationID=16,DiskNr=9, MovesO=49},
            new Optimal {ConfigurationID=16,DiskNr=10, MovesO=65},
            new Optimal {ConfigurationID=16,DiskNr=11, MovesO=81},
            new Optimal {ConfigurationID=16,DiskNr=12, MovesO=97},
            new Optimal {ConfigurationID=16,DiskNr=13, MovesO=113},
            new Optimal {ConfigurationID=16,DiskNr=14, MovesO=129},
            new Optimal {ConfigurationID=16,DiskNr=15, MovesO=161},

            new Optimal {ConfigurationID=17,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=17,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=17,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=17,DiskNr=4, MovesO=12},
            new Optimal {ConfigurationID=17,DiskNr=5, MovesO=19},
            new Optimal {ConfigurationID=17,DiskNr=6, MovesO=28},
            new Optimal {ConfigurationID=17,DiskNr=7, MovesO=37},
            new Optimal {ConfigurationID=17,DiskNr=8, MovesO=46},
            new Optimal {ConfigurationID=17,DiskNr=9, MovesO=57},
            new Optimal {ConfigurationID=17,DiskNr=10, MovesO=72},
            new Optimal {ConfigurationID=17,DiskNr=11, MovesO=87},
            new Optimal {ConfigurationID=17,DiskNr=12, MovesO=104},
            new Optimal {ConfigurationID=17,DiskNr=13, MovesO=125},
            new Optimal {ConfigurationID=17,DiskNr=14, MovesO=148},
            new Optimal {ConfigurationID=17,DiskNr=15, MovesO=173},
        

             new Optimal {ConfigurationID=18,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=18,DiskNr=2, MovesO=4},
            new Optimal {ConfigurationID=18,DiskNr=3, MovesO=7},
            new Optimal {ConfigurationID=18,DiskNr=4, MovesO=10},
            new Optimal {ConfigurationID=18,DiskNr=5, MovesO=17},
            new Optimal {ConfigurationID=18,DiskNr=6, MovesO=24},
            new Optimal {ConfigurationID=18,DiskNr=7, MovesO=31},
            new Optimal {ConfigurationID=18,DiskNr=8, MovesO=40},
            new Optimal {ConfigurationID=18,DiskNr=9, MovesO=51},
            new Optimal {ConfigurationID=18,DiskNr=10, MovesO=62},
            new Optimal {ConfigurationID=18,DiskNr=11, MovesO=77},
            new Optimal {ConfigurationID=18,DiskNr=12, MovesO=94},
            new Optimal {ConfigurationID=18,DiskNr=13, MovesO=113},
            new Optimal {ConfigurationID=18,DiskNr=14, MovesO=134},
            new Optimal {ConfigurationID=18,DiskNr=15, MovesO=155},

            new Optimal {ConfigurationID=19,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=19,DiskNr=2, MovesO=6},
            new Optimal {ConfigurationID=19,DiskNr=3, MovesO=10},
            new Optimal {ConfigurationID=19,DiskNr=4, MovesO=16},
            new Optimal {ConfigurationID=19,DiskNr=5, MovesO=24},
            new Optimal {ConfigurationID=19,DiskNr=6, MovesO=34},
            new Optimal {ConfigurationID=19,DiskNr=7, MovesO=44},
            new Optimal {ConfigurationID=19,DiskNr=8, MovesO=58},
            new Optimal {ConfigurationID=19,DiskNr=9, MovesO=72},
            new Optimal {ConfigurationID=19,DiskNr=10, MovesO=88},
            new Optimal {ConfigurationID=19,DiskNr=11, MovesO=106},
            new Optimal {ConfigurationID=19,DiskNr=12, MovesO=128},
            new Optimal {ConfigurationID=19,DiskNr=13, MovesO=152},
            new Optimal {ConfigurationID=19,DiskNr=14, MovesO=176},
            new Optimal {ConfigurationID=19,DiskNr=15, MovesO=200},

             new Optimal {ConfigurationID=20,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=20,DiskNr=2, MovesO=6},
            new Optimal {ConfigurationID=20,DiskNr=3, MovesO=12},
            new Optimal {ConfigurationID=20,DiskNr=4, MovesO=18},
            new Optimal {ConfigurationID=20,DiskNr=5, MovesO=26},
            new Optimal {ConfigurationID=20,DiskNr=6, MovesO=36},
            new Optimal {ConfigurationID=20,DiskNr=7, MovesO=48},
            new Optimal {ConfigurationID=20,DiskNr=8, MovesO=62},
            new Optimal {ConfigurationID=20,DiskNr=9, MovesO=78},
            new Optimal {ConfigurationID=20,DiskNr=10, MovesO=96},
            new Optimal {ConfigurationID=20,DiskNr=11, MovesO=114},
            new Optimal {ConfigurationID=20,DiskNr=12, MovesO=132},
            new Optimal {ConfigurationID=20,DiskNr=13, MovesO=156},
            new Optimal {ConfigurationID=20,DiskNr=14, MovesO=182},
            new Optimal {ConfigurationID=20,DiskNr=15, MovesO=212},

             new Optimal {ConfigurationID=21,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=21,DiskNr=2, MovesO=6},
            new Optimal {ConfigurationID=21,DiskNr=3, MovesO=10},
            new Optimal {ConfigurationID=21,DiskNr=4, MovesO=16},
            new Optimal {ConfigurationID=21,DiskNr=5, MovesO=24},
            new Optimal {ConfigurationID=21,DiskNr=6, MovesO=32},
            new Optimal {ConfigurationID=21,DiskNr=7, MovesO=44},
            new Optimal {ConfigurationID=21,DiskNr=8, MovesO=56},
            new Optimal {ConfigurationID=21,DiskNr=9, MovesO=70},
            new Optimal {ConfigurationID=21,DiskNr=10, MovesO=86},
            new Optimal {ConfigurationID=21,DiskNr=11, MovesO=104},
            new Optimal {ConfigurationID=21,DiskNr=12, MovesO=122},
            new Optimal {ConfigurationID=21,DiskNr=13, MovesO=144},
            new Optimal {ConfigurationID=21,DiskNr=14, MovesO=168},
            new Optimal {ConfigurationID=21,DiskNr=15, MovesO=194},

             new Optimal {ConfigurationID=22,DiskNr=1, MovesO=3},
            new Optimal {ConfigurationID=22,DiskNr=2, MovesO=8},
            new Optimal {ConfigurationID=22,DiskNr=3, MovesO=15},
            new Optimal {ConfigurationID=22,DiskNr=4, MovesO=24},
            new Optimal {ConfigurationID=22,DiskNr=5, MovesO=33},
            new Optimal {ConfigurationID=22,DiskNr=6, MovesO=46},
            new Optimal {ConfigurationID=22,DiskNr=7, MovesO=61},
            new Optimal {ConfigurationID=22,DiskNr=8, MovesO=76},
            new Optimal {ConfigurationID=22,DiskNr=9, MovesO=93},
            new Optimal {ConfigurationID=22,DiskNr=10, MovesO=116},
            new Optimal {ConfigurationID=22,DiskNr=11, MovesO=139},
            new Optimal {ConfigurationID=22,DiskNr=12, MovesO=166},
            new Optimal {ConfigurationID=22,DiskNr=13, MovesO=195},
            new Optimal {ConfigurationID=22,DiskNr=14, MovesO=226},
            new Optimal {ConfigurationID=22,DiskNr=15, MovesO=259},

             new Optimal {ConfigurationID=23,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=23,DiskNr=2, MovesO=4 },
            new Optimal {ConfigurationID=23,DiskNr=3, MovesO=9},
            new Optimal {ConfigurationID=23,DiskNr=4, MovesO=14},
            new Optimal {ConfigurationID=23,DiskNr=5, MovesO=23},
            new Optimal {ConfigurationID=23,DiskNr=6, MovesO=32},
            new Optimal {ConfigurationID=23,DiskNr=7, MovesO=43},
            new Optimal {ConfigurationID=23,DiskNr=8, MovesO=54},
            new Optimal {ConfigurationID=23,DiskNr=9, MovesO=69},
            new Optimal {ConfigurationID=23,DiskNr=10, MovesO=84},
            new Optimal {ConfigurationID=23,DiskNr=11, MovesO=101},
            new Optimal {ConfigurationID=23,DiskNr=12, MovesO=122},
            new Optimal {ConfigurationID=23,DiskNr=13, MovesO=147},
            new Optimal {ConfigurationID=23,DiskNr=14, MovesO=174},
            new Optimal {ConfigurationID=23,DiskNr=15, MovesO=201},

             new Optimal {ConfigurationID=30,DiskNr=1, MovesO=2},
            new Optimal {ConfigurationID=30,DiskNr=2, MovesO=8 },
            new Optimal {ConfigurationID=30,DiskNr=3, MovesO=26},
            new Optimal {ConfigurationID=30,DiskNr=4, MovesO=80},
            new Optimal {ConfigurationID=30,DiskNr=5, MovesO=242},
            new Optimal {ConfigurationID=30,DiskNr=6, MovesO=728},
            new Optimal {ConfigurationID=30,DiskNr=7, MovesO=2186},
            new Optimal {ConfigurationID=30,DiskNr=8, MovesO=6560},
            new Optimal {ConfigurationID=30,DiskNr=9, MovesO=19682},
             new Optimal {ConfigurationID=30,DiskNr=10, MovesO=59048},
            new Optimal {ConfigurationID=30,DiskNr=11, MovesO=177146},
            new Optimal {ConfigurationID=30,DiskNr=12, MovesO=531440},
            new Optimal {ConfigurationID=30,DiskNr=13, MovesO=1594322},
            new Optimal {ConfigurationID=30,DiskNr=14, MovesO=4782968},
            new Optimal {ConfigurationID=30,DiskNr=15, MovesO=14348906},

            new Optimal {ConfigurationID=31,DiskNr=1, MovesO=1},
            new Optimal {ConfigurationID=31,DiskNr=2, MovesO=4 },
            new Optimal {ConfigurationID=31,DiskNr=3, MovesO=13},
            new Optimal {ConfigurationID=31,DiskNr=4, MovesO=40},
            new Optimal {ConfigurationID=31,DiskNr=5, MovesO=121},
            new Optimal {ConfigurationID=31,DiskNr=6, MovesO=364},
            new Optimal {ConfigurationID=31,DiskNr=7, MovesO=1093},
            new Optimal {ConfigurationID=31,DiskNr=8, MovesO=3280},
            new Optimal {ConfigurationID=31,DiskNr=9, MovesO=9841},
            new Optimal {ConfigurationID=31,DiskNr=10, MovesO=29524},
            new Optimal {ConfigurationID=31,DiskNr=11, MovesO=88573},
            new Optimal {ConfigurationID=31,DiskNr=12, MovesO=265720},
            new Optimal {ConfigurationID=31,DiskNr=13, MovesO=797161},
            new Optimal {ConfigurationID=31,DiskNr=14, MovesO=2391484},
            new Optimal {ConfigurationID=31,DiskNr=15, MovesO=7174453},


          };
            foreach (Optimal o in Optimals)
            {
                context.Optimal.Add(o);
            }
            context.SaveChanges();


            
         

        

        }


    }
}
