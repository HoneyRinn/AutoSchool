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
    [Table(Name = "UsersAS")] 
    public class Users
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }
        [Column(Name = "fio")]
        public string fio { get; set; }
        [Column(Name = "login")]
        public string login { get; set; }
        [Column(Name = "password")]
        public string password { get; set; }
        [Column(Name = "role")]
        public string role { get; set; }
        [Column(Name = "status")]
        public bool status { get; set; }
    }
}