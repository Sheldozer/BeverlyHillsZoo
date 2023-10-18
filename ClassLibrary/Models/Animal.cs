using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public abstract class Animal
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public abstract void Move();


    }
}
