using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShoppingHub.Models.DTO
{

    public class BrandDTO
    {
        [Key]
        public int BrandId { get; set; }
        [StringLength(255)]
        public required string Name { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public string Image { get; set; }

    }
}
