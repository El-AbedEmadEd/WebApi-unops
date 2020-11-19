using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_unops.Model
{
    public class BookItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [ForeignKey("Authors")]
        public int Author { get; set; }
        [Required]
        public string Publisher { get; set; }
        public string Description { get; set; }

        public Authors Authors { get; set; }

    }
}
