using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPI_Sample.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int customerId { get; set; }
        public int repId { get; set; }
        [StringLength(10), Required]
        public string customerAccountNumber { get; set; }

        [NotMapped]
        public Employee employee { get; set; }
    }



}