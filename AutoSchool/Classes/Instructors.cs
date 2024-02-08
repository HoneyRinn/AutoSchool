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
    [Table(Name = "InstructorsAS")]
    public class Instructors
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }
        [Column(Name = "fio")]
        public string fio { get; set; }
        [Column(Name = "kval")]
        public string kval { get; set; }
        [Column(Name = "workAge")]
        public int workAge { get; set; }
        [Column(Name = "phoneNum")]
        public string phoneNum { get; set; }
        [Column(Name = "carNum")]
        public string carNum { get; set; }
        [Column(Name = "status")]
        public bool status { get; set; }
    }
}
