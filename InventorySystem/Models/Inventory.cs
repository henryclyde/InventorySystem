using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using InventorySystem.Models;

namespace InventorySystem.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; } // variable created for inventoryid

        [Required(ErrorMessage = "Please enter a product name.")] // required/error message
        public string ProductName { get; set; } = string.Empty; // variable created for productname

        [Required(ErrorMessage = "Please enter a date checked.")] // required/error message
        public DateTime DateChecked { get; set; }// variable created for datechecked

        [Required(ErrorMessage = "Please select a category.")] // required/error message
        public string CategoryId { get; set; } = string.Empty; // variable created for categoryid
        [ValidateNever]
        public Category Category { get; set; } = null!; // Category Class called

        [Required(ErrorMessage = "Please enter the number of items in stock")] // required/error message
        public int? NumberInStock { get; set; } // variable created for numberinstock

        [Required(ErrorMessage = "Please select a status.")] // required/error message
        public string StatusId { get; set; } = string.Empty; // variable created for statusid
        [ValidateNever] // validate never
        public Status Status { get; set; } = null!; // status class called

        public bool Overdue =>
            StatusId == "notacc" && DateChecked < DateTime.Today.AddDays(20); // overdue boolean variable established, true if over 20 days late and not accurate
    }
}
