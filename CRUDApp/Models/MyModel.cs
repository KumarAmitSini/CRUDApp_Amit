using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUDApp.Models
{
    public class MyModel
    {

        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public Nullable<int> salary { get; set; }

        [Required]
        public string city { get; set; }
        [Required]
        public string depart { get; set; }
    }
}