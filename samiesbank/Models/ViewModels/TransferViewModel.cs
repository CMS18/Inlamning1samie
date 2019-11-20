using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace samiesbank.Models.ViewModels
{
    public class TransferViewModel 
    {
        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "The account you're transferring money from")]
        public int TransferFromId { get; set; }

        [Required]
        [Display(Name = "The account you're transferring money to")]
        public int TransferToId { get; set; }

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }
    }
}