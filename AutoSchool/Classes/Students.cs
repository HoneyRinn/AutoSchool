//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
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
    [Table(Name = "StudentsAS")]
    public class Students
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }
        [Column(Name = "fio")]
        public string fio { get; set; }
        [Column(Name = "passport")]
        public long passport { get; set; }
        [Column(Name = "сategory")]
        public string сategory { get; set; }
        [Column(Name = "carTime")]
        public int carTime { get; set; }
        [Column(Name = "theoryExam")]
        public bool theoryExam { get; set; }
        [Column(Name = "gaiExam")]
        public bool gaiExam { get; set; }
        [Column(Name = "status")]
        public bool status { get; set; }
    }
}