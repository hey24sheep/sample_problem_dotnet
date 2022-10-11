using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApplication.Models
{
    public class ProductItemModel : ICloneable
    {
        [Range(0, Int64.MaxValue)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
