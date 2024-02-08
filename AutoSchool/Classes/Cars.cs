using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data;
using System.Data.Linq.Mapping;

namespace AutoSchool.Classes
{
    [Table(Name = "CarsAS")]
    public class Cars
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }
        [Column(Name = "name")]
        public string name { get; set; }
        [Column(Name = "model")]
        public string model { get; set; }
        [Column(Name = "age")]
        public int age { get; set; }
        [Column(Name = "regNum")]
        public string regNum { get; set; }
        [Column(Name = "milage")]
        public int milage { get; set; }
        [Column(Name = "category")]
        public string category { get; set; }
        [Column(Name = "status")]
        public bool status { get; set; }
    }
}
