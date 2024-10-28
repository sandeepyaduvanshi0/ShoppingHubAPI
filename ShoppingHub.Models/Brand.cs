using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ShoppingHub.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        [StringLength(255)]
        public required string Name { get; set; }  // Brand name
        [StringLength(500)]
        public string? Description { get; set; }
        public string? LogoImage { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
