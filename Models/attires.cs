using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace major_project.Models
{
    public class attires
    {

        public int ID { get; set; }
        [Required]
        [Display(Name = "Attire Name")]
        public string attire { get; set; }
        [Display(Name = "Availability")]
        public bool availability { get; set; }
        [Display(Name = "Damaged")]
        public bool damaged { get; set; }
        public attires()
        {
           damaged = false;
           availability = true;
        }
    }
}
